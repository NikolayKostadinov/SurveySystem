namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
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
    using System.Threading.Tasks;

    public class RolesController : AreaBaseController
    {
        public async Task<ActionResult> Index()
        {
            var roles = await this.Mediator.Send(new RolesPresentationQuery());
            return this.View(roles);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleCommand command)
        {
            if (this.ModelState.IsValid)
            {
                await this.Mediator.Send(command);

                return this.RedirectToAction("Index");

            }
            return this.View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteRoleCommand command)
        {
            await this.Mediator.Send(command);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> Edit(RoleForEditingQuery query)
        {
            var role = await this.Mediator.Send(query);
            return this.View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditRoleCommand command)
        {
            await this.Mediator.Send(command);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> GetNonMembers([DataSourceRequest] DataSourceRequest request, RoleNonMembersQuery query)
        {
            var result = await this.Mediator.Send(query);
            return Json(await result.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> GetMembers([DataSourceRequest] DataSourceRequest request, RoleMembersQuery query)
        {
            var result = await this.Mediator.Send(query);
            return Json(await result.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }
    }
}