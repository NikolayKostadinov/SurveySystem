//  ------------------------------------------------------------------------------------------------
//   <copyright file="EditUserCommandHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Commands.EditUser
{
    #region Using

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using Notifications.UserConfirmEmail;

    #endregion

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, UserListViewModel>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public EditUserCommandHandler(UserManager<User> userManager, IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<UserListViewModel> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException(nameof(user), request.Id);
            }

            var isEmailChanged = user.Email != request.Email;

            mapper.Map(request, user);
            user.EmailConfirmed = !isEmailChanged;


            var result = await userManager.UpdateAsync(user);

            result.Check(nameof(EditUserCommand));


            if (request.Roles != null)
            {
                var roles = request.Roles.Select(x => x.Name).ToArray();

                result = await userManager.AddToRolesAsync(user, roles);

                result.Check(nameof(EditUserCommand));
            }

            result = await userManager.UpdateSecurityStampAsync(user);

            result.Check(nameof(EditUserCommand));

            if (isEmailChanged)
            {
                await mediator.Publish(new UserConfirmEmailNotification(user), cancellationToken);
            }

            return mapper.Map<UserListViewModel>(user);
        }
    }
}