using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.LockUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.Core.Internal;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class LockUserCommandHandler : IRequestHandler<LockUserCommand>
    {
        private readonly UserManager<User> userManager;

        public LockUserCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            DateTimeOffset? lockOutEnd = (request.LockOperation == LockOperation.Lock) ? DateTimeOffset.MaxValue as DateTimeOffset? : null;
        
            var result = await userManager.SetLockoutEnabledAsync(user, true);

            result.Check(nameof(LockUserCommand));

            result = await userManager.SetLockoutEndDateAsync(user, lockOutEnd);

            result.Check(nameof(LockUserCommand));

            result = await userManager.UpdateSecurityStampAsync(user);

            result.Check(nameof(LockUserCommand));

            return await Unit.Task;
        }
    }
}
