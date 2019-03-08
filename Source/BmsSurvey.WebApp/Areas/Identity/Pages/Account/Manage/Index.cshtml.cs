namespace BmsSurvey.WebApp.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Application.Exceptions;
    using Application.Interfaces;
    using Application.Users.Commands.EditUser;
    using Application.Users.Queries.GetUser;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.Extensions.Localization;
    using WebApp.Pages;
    using LayoutResource = Resources.LayoutResource;

    public partial class IndexModel : PageModelBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer layoutLocalizer;
        private readonly IMapper mapper;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IStringLocalizerFactory factory,
            IMapper mapperParam)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.mapper = mapperParam;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public EditUserCommand Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.Mediator.Send(new GetUserQuery(User));
            Username = user.UserName;

            Input = new EditUserCommand
            {
                Id = user.Id,
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

            var result = await this.Mediator.Send(Input);

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
                values: new { userId, code },
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
