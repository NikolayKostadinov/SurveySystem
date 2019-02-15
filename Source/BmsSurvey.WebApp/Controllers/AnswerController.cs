using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Controllers
{
    using Application.Questions.Models;
    using Application.Surveys.Models;
    using Domain.Entities;
    using Newtonsoft.Json;

    public class AnswerController : BaseController
    {
        private readonly ISurveyDto surveyDto;

        public AnswerController(ISurveyDto surveyDto)
        {
            this.surveyDto = surveyDto;
        }

        public IActionResult Create(AnswerBindModel model)
        {
            this.surveyDto.AddQuestion(new QuestionSimpleViewModel(){Id = model.QuestionId, Value = model.Value});
            return Json(this.surveyDto.Questions.Values);
        }
    }

    public class AnswerBindModel
    {
        public int QuestionId  { get; set; }
        public string Value { get; set; }

    }
}