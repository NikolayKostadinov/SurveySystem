namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Localization;
    using LayoutResource = Resources.LayoutResource;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer layoutLocalizer;
        private readonly IMapper mapper;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IStringLocalizerFactory factory,
            IUserService userServiceParam,
            IMapper mapperParam)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.mapper = mapperParam;
            this.userService = userServiceParam;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "EMAIL_REQUIRED")]
            [EmailAddress(ErrorMessage = "EMAIL_INVALID")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

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

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }

            Username = user.UserName;

            Input = new InputModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                TabNumber = user.TabNumber,
                FirstName = user.FirstName,
                SirName = user.SirName,
                LastName = user.LastName,
            };

            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }

            var email = await userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var updateUser = new User()
            {
                Id = user.Id,
                TabNumber = Input.TabNumber,
                FirstName = Input.FirstName,
                SirName = Input.SirName,
                LastName = Input.LastName
            };

            var updateUserResult = await this.userService.UpdateUser(updateUser, User.Identity.Name);
            if (!updateUserResult.IsValid)
            {
                var userId = await userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred updating user with ID '{userId}'.");
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = layoutLocalizer["STATUS_PROFILE_UPDATED", userManager.GetUserId(User)];
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }


            var userId = await userManager.GetUserIdAsync(user);
            var email = await userManager.GetEmailAsync(user);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await emailSender.SendEmailAsync(
                email,
                layoutLocalizer["CONFIRM_YOUR_EMAIL"],
                layoutLocalizer["CONFIRM_YOUR_EMAIL_TEXT", HtmlEncoder.Default.Encode(callbackUrl)]);

            StatusMessage = layoutLocalizer["STATUS_UPDATE_PROFILE_EMAIL_SEND"];
            return RedirectToPage();
        }
    }
}
