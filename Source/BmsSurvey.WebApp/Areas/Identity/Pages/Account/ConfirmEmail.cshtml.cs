//  ------------------------------------------------------------------------------------------------
//   <copyright file="ConfirmEmail.cshtml.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account
{
    #region Using

    using System;
    using System.Threading.Tasks;
    using Application.Exceptions;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    #endregion

    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> userManager;

        public ConfirmEmailModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (HttpContext.Session.TryGetValue("Error", out var error))
            {
                return RedirectToAction("Index", "Home", new {area = ""});
            }

            if (userId == null || code == null)
                throw new OperationFailedException(new[] {$"Invalid user: '{userId}' or code '{code}'!"});


            var user = await userManager.FindByIdAsync(userId);
            if (user == null) throw new OperationFailedException(new[] {$"Unable to load user with ID '{userId}'."});

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");

            return Page();
        }
    }
}