namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Users.Commands.CreateUser;
    using Application.Users.Commands.UpdateUser;
    using Application.Users.Models;
    using Application.Users.Queries.GetAllUsersWithDeleted;
    using AutoMapper;
    using Common.Constants;
    using Common.Interfaces;
    using Domain.Entities.Identity;
    using Infrastructure.Extensions;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Persistence;
    using Resources;

    //using LayoutResource = Resources.LayoutResource;
    //using RoleSimpleViewModel = Application.Users.Models.RoleSimpleViewModel;

    public class UsersController : AreaBaseController
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ILogger<UsersController> logger;
        private readonly ILocalizationService<LayoutResource> localizationService;
        private readonly IUserService userService;

        public UsersController(BmsSurveyDbContext contextParam,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<UsersController> logger,
            ILocalizationService<LayoutResource> localizationService,
            IUserService userService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public IActionResult Index()
        {
            this.ViewData["roles"] = this.mapper.Map<IEnumerable<RoleSimpleViewModel>>(this.roleManager.Roles.ToList());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var users = await Mediator.Send(new GetAllUsersWithDeletedQuery());
            return Json(users.ToDataSourceResult(request, this.ModelState));
        }


        [HttpPost]
        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, CreateUserCommand model)
        {
            model.Password = GlobalConstants.DefaultPassword;
            if (ModelState.IsValid)
            {
                var result = await this.Mediator.Send(model);
            }

            return Json(await new List<CreateUserCommand> { model }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Lock([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => $"{y.ErrorMessage} - {y.Exception?.StackTrace}")).ToList();
                this.logger.LogError(string.Join("\n", errors));
                return Json(new { success = false, errorMessage = this.localizationService.GetLocalizedHtmlString("FAILED_OPERATION") });
            }

            return Json(await this.userService.LockAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Unlock([DataSourceRequest] DataSourceRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => $"{y.ErrorMessage} - {y.Exception?.StackTrace}")).ToList();
                this.logger.LogError(string.Join("\n", errors));
                return Json(new { success = false, errorMessage = this.localizationService.GetLocalizedHtmlString("FAILED_OPERATION") });
            }

            return Json(await this.userService.UnLockAsync(id));


        }


        [HttpPost]
        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, int id)
        {

            IStatus result = await this.userService.DeleteAsync(id, User.Identity.Name);

            if (!result.IsValid)
            {
                result.ToModelStateErrors(this.ModelState);
            }

            return Json(new List<object>().ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public async Task<IActionResult> Update([DataSourceRequest] DataSourceRequest request, UpdateUserCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this.Mediator.Send(command);
                    return Json(new List<UserListViewModel> { result }.ToDataSourceResult(request, ModelState));
                }
                catch (Exception ex)
                {
                    this.ModelState.AddModelError("", this.localizationService.GetLocalizedHtmlString("FAILED_OPERATION"));
                    this.logger.LogError(ex, this.localizationService.GetLocalizedHtmlString("FAILED_OPERATION"));
                    return Json(new List<UpdateUserCommand> { command }.ToDataSourceResult(request, ModelState));
                }

            }

            return Json(new List<UpdateUserCommand> { command }.ToDataSourceResult(request, ModelState));

        }
    }
}