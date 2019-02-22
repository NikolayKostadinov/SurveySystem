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
    using Infrastructure.Filters;
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

        public SurveyController(ILogger<SurveyController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}/{pagenum=1}")]
        [CompletedSurveyFilter]
        [CookieAcceptFilter]
        public async Task<IActionResult> Index(int id, int pagenum = 1)
        {
            try
            {
                var model = await this.Mediator.Send(new SurveyQuestionsQuery(id, pagenum));
                return View(model);
            }
            catch (NotFoundException nfe)
            {
                this.logger.LogError(nfe.Message);
                return View("SurveyNotFound", nfe.Key.ToString());
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
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View("ThankYouForm");
        }
    }
}