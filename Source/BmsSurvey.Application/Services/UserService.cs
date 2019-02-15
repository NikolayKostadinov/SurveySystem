//  ------------------------------------------------------------------------------------------------
//   <copyright file="UserService.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Services
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Abstract;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Persistence;
    using Resources;

    #endregion

    public class UserService : IUserService
    {
        private readonly BmsSurveyDbContext data;
        private readonly IStatusFactory statusFactory;
        private readonly UserManager<User> userManager;
        private readonly ILocalizationService<MessageResource> messageLocalization;

        public UserService(UserManager<User> userManager, BmsSurveyDbContext data,
            IStatusFactory statusFactory, ILocalizationService<MessageResource> messageLocalization)
        {
            this.userManager = userManager;
            this.data = data;
            this.statusFactory = statusFactory;
            this.messageLocalization = messageLocalization;
        }

        public string GetUserId(ClaimsPrincipal user)
        {
            return userManager.GetUserId(user);
        }

        public IQueryable<User> GetAllUsers()
        {
            return data.Users
                .Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role);
        }

        public IQueryable<User> GetAllUsersWithDeleted()
        {
            return data.Users
                .Include(x => x.UserRoles)
                .ThenInclude(ur => ur.Role);
        }

        public async Task<object> LockAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return await UserLock(user, DateTimeOffset.MaxValue);
        }

        public async Task<object> UnLockAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return await UserLock(user, null);
        }

        public async Task<IStatus> DeleteAsync(int id, string userName)
        {
            var dbUser = await data.Users.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            var status = this.statusFactory.GetInstance(typeof(IStatus));
            if (dbUser.UserName == userName)
            {
                var errors = new List<ValidationResult> { new ValidationResult(this.messageLocalization.GetLocalizedHtmlString("CANNOT_ERASE_YOUR_SELF")) };
                status.SetErrors(errors);
                return status;
            }

            var identityResult = await this.userManager.DeleteAsync(dbUser).ConfigureAwait(false);
            var result = this.statusFactory.GetInstance(typeof(IStatus));

            if (identityResult.Succeeded)
            {
                return result;
            }

            status.SetErrors(
                identityResult.Errors.Select(x => new ValidationResult(x.Description)));

            return status;

        }

        public async Task<IStatus> UpdateUser(User user, string userName)
        {
            var status = this.statusFactory.GetInstance(typeof(IStatus));
            var identityErrors = new List<IdentityError>();
            var dbUser = userManager.Users
                              .Include(u => u.UserRoles)
                              .ThenInclude(ur => ur.Role)
                              .FirstOrDefault(x => x.Id == user.Id);

            if (dbUser == null)
            {

                var errors = new List<ValidationResult>
                        {
                            new ValidationResult(this.messageLocalization.GetLocalizedHtmlString("USER_NOT_FOUND"))
                        };
                status.SetErrors(errors);
                return status;
            }

            dbUser.TabNumber = user.TabNumber;
            dbUser.FirstName = user.FirstName;
            dbUser.SirName = user.SirName;
            dbUser.LastName = user.LastName;

            var rolesToRemove = dbUser.UserRoles.Select(x => x.Role.Name).ToList();
            if (rolesToRemove.Count > 0)
            {
                identityErrors.AddRange((await this.userManager.RemoveFromRolesAsync(dbUser, rolesToRemove)).Errors);
            }

            var roleList = user.UserRoles.Select(ur => ur.RoleId).ToList();
            var newRoles = this.data.Roles.Where(x => roleList.Contains(x.Id)).Select(x => x.Name)
                .ToList();

            var operationResult = await this.userManager.AddToRolesAsync(dbUser, newRoles);
            identityErrors.AddRange(operationResult.Errors);

            operationResult = await userManager.UpdateAsync(dbUser);
            identityErrors.AddRange(operationResult.Errors);

            operationResult = await userManager.UpdateSecurityStampAsync(dbUser);
            identityErrors.AddRange(operationResult.Errors);


            if (identityErrors.Any())
            {
                var errors = identityErrors.Select(x => new ValidationResult(x.Description));
                status.SetErrors(errors);
            }
            else
            {
                status.Result = dbUser;
            }

            //SetHistoricChanges(dbUser, user);
            //var result = this.data.SaveChanges(userName);

            //if (!result.IsValid)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        status.AppendError(error);
            //    }
            //}

            return status;
        }

        public async Task<IStatus> CreateAsync(User user, string password)
        {
            var identityResult = await this.userManager.CreateAsync(user, password).ConfigureAwait(false);
            var result = this.statusFactory.GetInstance(typeof(IStatus));
            if (identityResult.Succeeded)
            {
                return result;
            }

            result.SetErrors(
                identityResult.Errors.Select(x => new ValidationResult(x.Description)));

            return result;
        }


        public User GetUser(ClaimsPrincipal user)
        {
            return this.userManager.Users
                .FirstOrDefault(x => x.UserName == user.Identity.Name);
        }


        public IStatus UpdateUserCulture(int userId, string uICulture)
        {
            var dbUser = userManager.Users.FirstOrDefault(x => x.Id == userId);
            dbUser.CultureId = uICulture;
            data.Users.Update(dbUser);
            data.SaveChanges();
            return this.statusFactory.GetInstance(typeof(IStatus));

        }

        private async Task<object> UserLock(User user, DateTimeOffset? lockOutEnd)
        {
            var result = await userManager.SetLockoutEnabledAsync(user, true);
            var result1 = await userManager.SetLockoutEndDateAsync(user, lockOutEnd);

            if (result.Succeeded && result1.Succeeded)
                return new { success = true };
            return new
            {
                success = false,
                errors = result.Errors.Select(x => x.Description)
                    .Union(result1.Errors.Select(x => x.Description)).ToArray()
            };
        }
    }
}