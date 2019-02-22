//  ------------------------------------------------------------------------------------------------
//   <copyright file="IUserCreationMessageProvider.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Notifications.Models;

    public interface IUserCreationMessageService
    {
        Task<Message> GetMessageAsync(User user);
    }
}