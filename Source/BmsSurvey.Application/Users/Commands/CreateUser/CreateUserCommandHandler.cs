//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateUserCommandHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    #region Using

    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Notifications.UserConfirmEmail;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public CreateUserCommandHandler(UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = new User()
            {
                TabNumber = request.TabNumber,
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                SirName = request.SirName,
                LastName = request.LastName,
            };

            var result = await this.userManager.CreateAsync(dbUser, request.Password);

            result.Check(nameof(CreateUserCommand));

            var user = await this.userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.UserName);
            }

            var roles = request.Roles?.Select(x => x.Name).ToList() ?? new List<string>();

            if (roles.Any())
            {
                result = await this.userManager.AddToRolesAsync(user, roles);
                result.Check(nameof(CreateUserCommand));
            }

            await this.mediator.Publish(new UserConfirmEmailNotification(user), cancellationToken);

            return await Unit.Task;
        }
    }
}