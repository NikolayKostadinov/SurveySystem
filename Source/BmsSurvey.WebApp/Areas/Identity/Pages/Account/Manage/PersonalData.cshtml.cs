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

    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<PersonalDataModel> logger;
        private readonly IStringLocalizer layoutLocalizer;

        public PersonalDataModel(
            UserManager<User> userManager,
            ILogger<PersonalDataModel> logger,
            IStringLocalizerFactory factory)
        {
            this.userManager = userManager;
            this.logger = logger;

            var type = typeof(LayoutResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            layoutLocalizer = factory.Create("LayoutResource", assemblyName.Name);
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(layoutLocalizer["USER_NOTFOUND", userManager.GetUserId(User)]);
            }

            return Page();
        }
    }
}