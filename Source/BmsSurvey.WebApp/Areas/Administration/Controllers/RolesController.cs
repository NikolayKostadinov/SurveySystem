namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Models.ViewModels;
    using Persistence;

    public class RolesController : AreaBaseController
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly BmsSurveyDbContext context;


        public RolesController(IMapper mapperParam,
            UserManager<User> userManagerParam, RoleManager<Role> roleManagerParam, BmsSurveyDbContext contextParam)
        {
            this.mapper = mapperParam ?? throw new ArgumentNullException(nameof(mapperParam));
            this.userManager = userManagerParam;
            this.roleManager = roleManagerParam;
            this.context = contextParam;
        }
        public ActionResult Index()
        {
            var roles = this.mapper.Map<IEnumerable<RolePresentationViewModel>>(this.context.Roles.ToList());
            var users = this.context.Users;

            foreach (var role in roles)
            {
                role.Users = users.Where(x => x.UserRoles.Any(y => y.RoleId == role.Id)).Select(x => x.UserName);
            }

            return this.View(roles);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var role = this.mapper.Map<Role>(model);
                IdentityResult result = await this.roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.AddErrorsFromResult(result);
                }
            }
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            Role role = await this.roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                IdentityResult result = await this.roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Index");
                }
                else
                {
                    return this.View("Error");//, result.Errors.Select(x=>x.Description));
                }
            }
            else
            {
                return this.View("Error");//, new string[] { "Ролята не е открита!" });
            }
        }

        /// <summary>
        /// Adds the errors from result.
        /// </summary>
        /// <param name="result">The result.</param>
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors.Select(x => x.Description))
            {
                this.ModelState.AddModelError("", error);
            }
        }



        public async Task<ActionResult> Edit(int? id)
        {
            id = id ?? 0;
            Role role = await this.roleManager.Roles.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == id);
            int[] memberIds = role.UserRoles.Select(x => x.UserId).ToArray();
            var auMembers = this.context.Users.ToList().Where(x => memberIds.Any(y => y == x.Id) && x.IsDeleted == false).ToList();
            IEnumerable<UserSimpleViewModel> members = this.mapper.Map<IEnumerable<UserSimpleViewModel>>(auMembers);
            IEnumerable<UserSimpleViewModel> nonMembes = this.mapper.Map<IEnumerable<UserSimpleViewModel>>(this.context.Users.Where(x => x.IsDeleted == false).ToList().Except(auMembers));
            return this.View(new RoleEditViewModel
            {
                Role = role,
                Name = role.Name,
                Description = role.Description,
                Members = members,
                NonMembers = nonMembes
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleModificationViewModel model)
        {
            IdentityResult result;
            if (this.ModelState.IsValid)
            {
                var role = await this.roleManager.FindByNameAsync(model.RoleName);
                if (role != null)
                {
                    role.Name = model.Name;
                    role.Description = model.Description;
                    result = await this.roleManager.UpdateAsync(role);
                    if (!result.Succeeded)
                    {
                        return this.View("Error");//, result.Errors);
                    }
                }
                else
                {
                    return this.View("Error");//, new string[] { "Ролята не е намерена!" });
                }

                foreach (int userId in model.IdsToAdd ?? new int[] { })
                {
                    var user = await this.userManager.FindByIdAsync(userId.ToString());
                    result = await this.userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return this.View("Error");//, result.Errors);
                    }

                    result = await userManager.UpdateSecurityStampAsync(user);
                    if (!result.Succeeded)
                    {
                        return this.View("Error");//, result.Errors);
                    }
                }

                foreach (int userId in model.IdsToDelete ?? new int[] { })
                {
                    var user = await this.userManager.FindByIdAsync(userId.ToString());

                    result = await this.userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return this.View("Error");//, result.Errors);
                    }

                    result = await userManager.UpdateSecurityStampAsync(user);
                    if (!result.Succeeded)
                    {
                        return this.View("Error");//, result.Errors);
                    }
                }
                return this.RedirectToAction("Index");
            }
            return this.View("Error");//, new string[] { "Ролята не е намерена!" });
        }
    }
}