namespace BmsSurvey.WebApp.Areas.Administration.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Area("Administration")]
    public class AdministrationModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!User.IsInRole("Administrator")&&User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Account/AccessDenied",new{area="Identity"});
            }else if (!User.IsInRole("Administrator") && !User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Account/Login", new { area = "Identity" });
            }

            return Page();
        }
    }
}