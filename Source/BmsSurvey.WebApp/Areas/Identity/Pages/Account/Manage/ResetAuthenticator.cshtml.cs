namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account.Manage
{
    using System.Reflection;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using LayoutResource = Resources.LayoutResource;

    public class ResetAuthenticatorModel : PageModel
    {
        UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        ILogger<ResetAuthenticatorModel> logger;
        private readonly IStringLocalizer layoutLocalizer;

        public ResetAuthenticatorModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<ResetAuthenticatorModel> logger,
            IStringLocalizerFactory factory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }

            await userManager.SetTwoFactorEnabledAsync(user, false);
            await userManager.ResetAuthenticatorKeyAsync(user);
            logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = layoutLocalizer["RESET_AUTHENTICATOR_STATUS"];

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}