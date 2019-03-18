using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Models.EvaluationModels;

    class FreeTextAnswerQuestionEvaluationHandler : QuestionEvaluationHandler
    {
        private readonly IMapper mapper;

        public FreeTextAnswerQuestionEvaluationHandler(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task<bool> CanHandleAsync(Question question) =>
            Task.FromResult(question.QuestionType == QuestionType.FreeText);


        protected override async Task<BaseQuestionEvaluationModel> HandleAsync(Question question)
        {
            var result = this.mapper.Map<FreeTextAnswerQuestionEvaluationModel>(question);
            var answers = question.Answers.OfType<FreeTextAnswer>().Select(x=>new FreeAnswerViewModel(){Answer = x.Value, Email = x.CreatedFrom});
            result.Answers = answers;
            return await Task.FromResult(result);
        }
    }
}
