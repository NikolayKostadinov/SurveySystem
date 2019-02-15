namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Controllers;

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public abstract class AreaBaseController : BaseController
    {
       
    }
}