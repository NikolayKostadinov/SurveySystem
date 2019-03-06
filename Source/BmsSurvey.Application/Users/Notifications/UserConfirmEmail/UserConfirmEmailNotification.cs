//  ------------------------------------------------------------------------------------------------
//   <copyright file="UserCreatedNotification.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Notifications.UserConfirmEmail
{
    #region Using

    using Domain.Entities.Identity;
    using MediatR;

    #endregion

    public class UserConfirmEmailNotification : INotification
    {
        public UserConfirmEmailNotification(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}