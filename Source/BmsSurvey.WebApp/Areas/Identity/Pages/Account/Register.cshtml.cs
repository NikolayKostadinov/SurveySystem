namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Constants;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using LayoutResource = Resources.LayoutResource;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer layoutLocalizer;
        private readonly IMapper mapper;


        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IStringLocalizerFactory factory,
            IMapper mapperParam)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.mapper = mapperParam;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<SelectListItem> SuppurtedCultures => GlobalConstants.SupportedCultures
            .Select(x => new SelectListItem(x.Value.NativeName, x.Key)).ToList();


        public class InputModel
        {
            [Required(ErrorMessage = "TABNUMBER_REQUIRED")]
            [StringLength(5, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
            [Display(Name = "TABNUMBER")]
            public string TabNumber { get; set; }

            [Required(ErrorMessage = "FIRSTNAME_REQUIRED")]
            [MinLength(2)]
            [Display(Name = "FIRSTNAME")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "SIRNAME_REQUIRED")]
            [MinLength(2)]
            [Display(Name = "SIRNAME")]
            public string SirName { get; set; }

            [Required(ErrorMessage = "LASTNAME_REQUIRED")]
            [MinLength(2)]
            [Display(Name = "LASTNAME")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "USERNAME_REQUIRED")]
            [MinLength(4)]
            [Display(Name = "USERNAME")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "EMAIL_REQUIRED")]
            [EmailAddress(ErrorMessage = "EMAIL_INVALID")]
            public string Email { get; set; }

            [Required(ErrorMessage = "PASSWORD_REQUIRED")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "CONFIRM_PASSWORD_NOT_MATCHING")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "CULTURE")]
            [Required]
            public string CultureId { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            // System.Globalization.CultureInfo.CurrentCulture
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(System.Globalization.CultureInfo.CurrentCulture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    SirName = Input.SirName,
                    LastName = Input.LastName,
                    CultureId = Input.CultureId,
                };

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(
                        Input.Email,
                        layoutLocalizer["CONFIRM_YOUR_EMAIL"],
                        layoutLocalizer["CONFIRM_YOUR_EMAIL_TEXT", HtmlEncoder.Default.Encode(callbackUrl)]);

                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
