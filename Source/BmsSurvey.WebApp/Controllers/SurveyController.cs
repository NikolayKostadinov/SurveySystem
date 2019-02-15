using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Controllers
{
    using System.Runtime.InteropServices.WindowsRuntime;
    using Application.Exceptions;
    using Application.Surveys.Queries.GetSurveyQuestions;

    [Route("{culture=bg}/survey")]
    [Route("survey")]
    public class SurveyController : BaseController
    {
        [HttpGet("{id}/{pagenum?}")]
        public async Task<IActionResult> Index(int id, int pagenum = 1)
        {
            try
            {
                var model = await this.Mediator.Send(new SurveyQuestionsQuery(id, pagenum));
                return View(model);
            }
            catch (NotFoundException nfe)
            {
                return View("SurveyNotFound", nfe.Key.ToString());
            }
        }

        [HttpGet("{id}/previouse/{current}")]
        public async Task<IActionResult> Previous(int id, int current)
        {
            return RedirectToAction("Index", new { id = id, pagenum = current - 1 });
        }

        [HttpGet("{id}/next/{current}")]
        public async Task<IActionResult> Next(int id, int current)
        {
            return RedirectToAction("Index", new { id = id, pagenum = current + 1 });
        }
    }
}