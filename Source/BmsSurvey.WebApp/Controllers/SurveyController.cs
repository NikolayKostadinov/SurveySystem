using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Controllers
{
    using System.Runtime.InteropServices.WindowsRuntime;
    using Application.Exceptions;
    using Application.Surveys.Commands.SaveSurvey;
    using Application.Surveys.Queries.GetAllSurveys;
    using Application.Surveys.Queries.GetSurveyQuestionsWithAnswers;
    using Infrastructure.Interfaces;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.Logging;

    [Route("{culture=bg}/survey")]
    [Route("survey")]
    public class SurveyController : BaseController
    {
        private readonly ILogger<SurveyController> logger;
        private readonly ITrackingConsentFeature consentFeature;

        public SurveyController(ILogger<SurveyController> logger, IHttpContextAccessor accessor)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.consentFeature = accessor.HttpContext.Features.Get<ITrackingConsentFeature>();
        }

        [HttpGet("{id}/{pagenum=1}")]
        public async Task<IActionResult> Index(int id, [FromServices]IIpProvider ipAddressProvider, int pagenum = 1)
        {
            try
            {
                var userAgreesCookies = consentFeature?.CanTrack ?? true;
                if (userAgreesCookies)
                {
                    var model = await this.Mediator.Send(new SurveyQuestionsQuery(id, pagenum, ipAddressProvider.GetIp()));
                    return View(model);
                }
                else
                {
                    return RedirectToAction("AcceptCookies", new {retunUrl = Url.Action("Index")});
                }

            }
            catch (NotFoundException nfe)
            {
                this.logger.LogError(nfe.Message);
                return View("SurveyNotFound", nfe.Key.ToString());
            }
            catch (SurveyCompletedException sce)
            {
                this.logger.LogError(sce.Message);
                return View("ThankYouForm");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(int id, string email, [FromServices]IIpProvider ipAddressProvider)
        {
            var userName = email;
            if (string.IsNullOrEmpty(userName))
            {
                userName = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous";
            }
            var model = await this.Mediator.Send(new SaveSurveyCommand(id, userName, email, ipAddressProvider.GetIp()));
            return View("ThankYouForm", model);
        }

        public IActionResult AcceptCookies(string retunUrl)
        {
            return View((object)retunUrl);
        }
    }
}