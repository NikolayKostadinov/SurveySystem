//  ------------------------------------------------------------------------------------------------
//   <copyright file="AnswerController.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Controllers
{
    #region Using

    using Application.Answers.Models;
    using Application.Surveys.Models;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class AnswerController : BaseController
    {
        private readonly ISurveyDto surveyDto;

        public AnswerController(ISurveyDto surveyDto)
        {
            this.surveyDto = surveyDto;
        }

        public IActionResult Create(AnswerViewModel model)
        {
            surveyDto.AddAnswer(model);
            return Json(surveyDto.Answers.Values);
        }
    }
}