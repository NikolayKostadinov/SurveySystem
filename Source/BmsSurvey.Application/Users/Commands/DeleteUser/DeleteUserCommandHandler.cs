using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.DeleteUser
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<User> userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = int.Parse(this.userManager.GetUserId(new ClaimsPrincipal(request.CurrentUser)));

            if (request.Id == currentUserId)
            {
                throw new OperationFailedException(new[] { $"Cannot delete same user {request.CurrentUser.Identity.Name}" });
            }

            var user = await this.userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            var result = await userManager.DeleteAsync(user);
            result.Check(nameof(DeleteUserCommand));

            result = await userManager.UpdateSecurityStampAsync(user);
            result.Check(nameof(DeleteUserCommand));


            return await Unit.Task;
        }
    }
}
