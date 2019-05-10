//  ------------------------------------------------------------------------------------------------
//   <copyright file="RolesController.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    #region Using

    using System.Threading.Tasks;
    using Application.Roles.Commands.CreateRole;
    using Application.Roles.Commands.DeleteRole;
    using Application.Roles.Commands.EditRole;
    using Application.Roles.Queries.GetAllRolesPresentation;
    using Application.Roles.Queries.GetRoleForEditing;
    using Application.Roles.Queries.GetRoleMembers;
    using Application.Roles.Queries.GetRoleNonMembers;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class RolesController : AreaBaseController
    {
        public async Task<ActionResult> Index()
        {
            var roles = await Mediator.Send(new RolesPresentationQuery());
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Send(command);

                return RedirectToAction("Index");
            }

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(RoleForEditingQuery query)
        {
            var role = await Mediator.Send(query);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GetNonMembers([DataSourceRequest] DataSourceRequest request,
            RoleNonMembersQuery query)
        {
            var result = await Mediator.Send(query);
            return Json(await result.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> GetMembers([DataSourceRequest] DataSourceRequest request,
            RoleMembersQuery query)
        {
            var result = await Mediator.Send(query);
            return Json(await result.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }
    }
}