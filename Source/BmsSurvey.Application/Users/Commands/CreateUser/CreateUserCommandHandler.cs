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

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Notifications.UserConfirmEmail;

    #endregion

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IStatus>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IStatusFactory statusFactory;

        public CreateUserCommandHandler(
            UserManager<User> userManager,
            IMapper mapper,
            IStatusFactory statusFactory,
            IMediator mediator)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.statusFactory = statusFactory;
            this.mediator = mediator;
        }

        public async Task<IStatus> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
            var identityResult = await this.userManager.CreateAsync(dbUser, request.Password);
            var result = this.statusFactory.GetInstance(typeof(IStatus));
            if (!identityResult.Succeeded)
            {
                result.SetErrors(
                    identityResult.Errors.Select(x => new ValidationResult(x.Description)));
                return result;
            }

            var user = await this.userManager.FindByNameAsync(request.UserName);
            var roles = request?.Roles?.Select(x => x.Name)?.ToList() ?? new List<string>();
            if (roles.Any() && user != null)
            {
                identityResult = await this.userManager.AddToRolesAsync(user, roles);
                result.SetErrors(
                    identityResult.Errors.Select(x => new ValidationResult(x.Description)));
            }

            if (result.IsValid)
            {
                await this.mediator.Publish(new UserConfirmEmailNotification(user), cancellationToken);
            }

            return result;
        }
    }
}