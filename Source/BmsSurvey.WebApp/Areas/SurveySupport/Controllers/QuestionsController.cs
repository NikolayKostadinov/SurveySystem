namespace BmsSurvey.WebApp.Areas.SurveySupport.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Exceptions;
    using Application.Questions.Commands.CreateQuestion;
    using Application.Questions.Commands.EditQuestion;
    using Application.Questions.Models;
    using Application.Questions.Queries.GetAllQuestionsForSurvey;
    using Application.Surveys.Queries.GetSurveyById;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionsController : AreaBaseController
    {
        public async Task<IActionResult> Index(int id = 0)
        {
            if (id == 0)
            {
                return View("SurveyNotFound", id.ToString());
            }

            var model = await this.Mediator.Send(new GetSurveyByIdQuery(id));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([DataSourceRequest]DataSourceRequest request, QuestionsForSurveyQuery model)
        {
            var questions = await this.Mediator.Send(model);
            return Json(await questions.ToDataSourceResultAsync(request, this.ModelState).ConfigureAwait(false));
        }
        [HttpPost]
        public async Task<IActionResult> Create([DataSourceRequest]DataSourceRequest request, CreateQuestionCommand model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = await this.Mediator.Send(model);
                    return Json(await new List<QuestionListViewModel> { result }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
                }
                return Json(await new List<CreateQuestionCommand> { model }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
            }
            catch (NotFoundException nfe)
            {
                return View("SurveyNotFound", nfe.Key.ToString());
            }
        }

        [HttpPost]
        public IActionResult Delete()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Update([DataSourceRequest]DataSourceRequest request, EditQuestionCommand model)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = await this.Mediator.Send(model);
                    return Json(await new List<QuestionListViewModel> { result }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
                }
                return Json(await new List<CreateQuestionCommand> { model }.ToDataSourceResultAsync(request, ModelState).ConfigureAwait(false));
            }
            catch (NotFoundException nfe)
            {
                return View("SurveyNotFound", nfe.Key.ToString());
            }
        }

        public IActionResult Create()
        {
            throw new System.NotImplementedException();
        }
    }
}