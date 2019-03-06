//  ------------------------------------------------------------------------------------------------
//   <copyright file="UserCreatedHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Notifications.UserConfirmEmail
{
    #region Using

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using MediatR;

    #endregion

    public class UserConfirmEmailNotificationHandler : INotificationHandler<UserConfirmEmailNotification>
    {
        private readonly IUserCreationMessageService creationMessageService;
        private readonly IMailNotificationService mailNotificationService;

        public UserConfirmEmailNotificationHandler(IMailNotificationService mailNotificationService,
            IUserCreationMessageService creationMessageService)
        {
            this.mailNotificationService = mailNotificationService ??
                                           throw new ArgumentNullException(nameof(mailNotificationService));
            this.creationMessageService = creationMessageService ??
                                          throw new ArgumentNullException(nameof(creationMessageService));
        }


        public async Task Handle(UserConfirmEmailNotification notification, CancellationToken cancellationToken)
        {
            var message = await creationMessageService.GetMessageAsync(notification.User);

            await mailNotificationService.SendAsync(message);
        }
    }
}