namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using LayoutResource = Resources.LayoutResource;

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<LoginModel> logger;
        private readonly IStringLocalizer layoutLocalizer;
        private readonly ILocalizationUrlService localizationUrlService;
        private readonly ISupportedCulturesService supportedCulturesProvider;


        public LoginModel(SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<LoginModel> logger,
            IStringLocalizerFactory factory,
            ILocalizationUrlService localizationUrlService,
            ISupportedCulturesService supportedCulturesProvider)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.userManager = userManager;
            this.localizationUrlService = localizationUrlService;
            this.supportedCulturesProvider = supportedCulturesProvider;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "USERNAME_REQUIRED")]
            [MinLength(4, ErrorMessage = "USERNAME_INVALID")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "PASSWORD_REQUIRED")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //Todo: Fix deleted login
            returnUrl = returnUrl ?? Url.Content("/");

            if (ModelState.IsValid)
            {
                var user = await this.userManager.FindByNameAsync(Input.UserName);
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded && !user.IsDeleted)
                {
                    var userCulture = user?.CultureId ?? this.supportedCulturesProvider.SystemDefaultCulture;
                    this.supportedCulturesProvider.SetApplicationCulture(userCulture);
                    returnUrl = localizationUrlService.GetLocalizedUri(new Uri($"{Request.Scheme}://{Request.Host}{returnUrl}"), userCulture).AbsoluteUri;

                    logger.LogInformation("User logged in.");
                    return Redirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, layoutLocalizer["INVALID_LOGIN_ATTEMPT"]);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
