using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Areas.SurveySupport.Controllers
{
    using Application.Exceptions;
    using Application.Surveys.Commands.CreateSurvey;
    using Application.Surveys.Commands.DeleteSurvey;
    using Application.Surveys.Commands.EditSurvey;
    using Application.Surveys.Queries.GetAllSurveys;
    using Domain.Entities.Identity;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Http.Features;

    public class SurveyController : AreaBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyCommand model)
        {
            if (this.ModelState.IsValid)
            {
                await this.Mediator.Send(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var surveys = await this.Mediator.Send(new AllSurveysQuery());
            return Json(await surveys.ToDataSourceResultAsync(request, this.ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, DeleteSurveyCommand model)
        {
            if (this.ModelState.IsValid)
            {
                await this.Mediator.Send(model);
            }

            return Json(await new List<object>()
                .ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public async Task<IActionResult> Update([DataSourceRequest] DataSourceRequest request, EditSurveyCommand model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.Mediator.Send(model);

            }

            return Json(await new List<EditSurveyCommand> { model }
                .ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
        }

        [HttpPost]
        public IActionResult GetSurveyUrl(int id)
        {
            var url = Url.Action("Index", "Survey", new { area = "", id = id }, Request.Scheme, Request.Host.Value);
            return Json(new { url = url });
        }
    }
}