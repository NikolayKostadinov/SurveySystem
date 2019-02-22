//  ------------------------------------------------------------------------------------------------
//   <copyright file="UserCreatedNotification.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Commands.CreateUser
{
    #region Using

    using Domain.Entities.Identity;
    using MediatR;
    using Microsoft.Extensions.Logging;

    #endregion

    public class UserCreatedNotification : INotification
    {
        public UserCreatedNotification(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}