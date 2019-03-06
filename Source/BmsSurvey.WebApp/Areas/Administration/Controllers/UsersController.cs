//  ------------------------------------------------------------------------------------------------
//   <copyright file="UsersController.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Roles.Models;
    using Application.Roles.Queries.GetAllRolesDropdownData;
    using Application.Users.Commands.CreateUser;
    using Application.Users.Commands.DeleteUser;
    using Application.Users.Commands.EditUser;
    using Application.Users.Commands.LockUser;
    using Application.Users.Commands.SendConfirmationEmail;
    using Application.Users.Models;
    using Application.Users.Queries.GetAllUsersWithDeleted;
    using Common.Constants;
    using Common.Interfaces;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Mvc;
    using Resources;

    #endregion

    public class UsersController : AreaBaseController
    {
        private readonly ILocalizationService<LayoutResource> localizationService;

        public UsersController(ILocalizationService<LayoutResource> localizationService)
        {
            this.localizationService =
                localizationService ?? throw new ArgumentNullException(nameof(localizationService));
        }

        public async Task<IActionResult> Index()
        {
            var roles = await Mediator.Send<IEnumerable<RoleSimpleViewModel>>(new GetAllRolesDropdownDataQuery());
            ViewData["roles"] = roles.ToArray();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var users = await Mediator.Send(new GetAllUsersWithDeletedQuery());
            return Json(await users.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }


        [HttpPost]
        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, CreateUserCommand model)
        {
            model.Password = GlobalConstants.DefaultPassword;
            if (ModelState.IsValid)
            {
                await Mediator.Send(model);
            }

            return Json(await new List<CreateUserCommand> {model}.ToDataSourceResultAsync(request, ModelState)
                .ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Lock(int id)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new LockUserCommand(id, LockOperation.Lock));
                return Json(new {success = true});
            }

            return Json(new
                {success = false, errorMessage = localizationService.GetLocalizedHtmlString("FAILED_OPERATION")});
        }

        [HttpPost]
        public async Task<IActionResult> Unlock(int id)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new LockUserCommand(id, LockOperation.Unlock));
                return Json(new {success = true});
            }

            return Json(new
                {success = false, errorMessage = localizationService.GetLocalizedHtmlString("FAILED_OPERATION")});
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(new DeleteUserCommand(id, User));
                return Json(new {success = true});
            }

            return Json(new
                {success = false, errorMessage = localizationService.GetLocalizedHtmlString("FAILED_OPERATION")});
        }

        [HttpPost]
        public async Task<IActionResult> Update([DataSourceRequest] DataSourceRequest request, EditUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                return Json(await new List<UserListViewModel> {result}.ToDataSourceResultAsync(request, ModelState)
                    .ConfigureAwait(false));
            }

            return Json(await new List<EditUserCommand> {command}.ToDataSourceResultAsync(request, ModelState)
                .ConfigureAwait(false));
        }

        public async Task<IActionResult> SendConfirmationEmail(SendConfirmationEmailCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);
                return Json(new {success = true});
            }

            return Json(new
                {success = false, errorMessage = localizationService.GetLocalizedHtmlString("FAILED_OPERATION")});
        }
    }
}