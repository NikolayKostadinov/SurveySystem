//  ------------------------------------------------------------------------------------------------
//   <copyright file="Rate1to5StarsQuestionEvaluationHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Constants;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Models.EvaluationModels;
    using Remotion.Linq.Clauses.ResultOperators;

    public class Rate1To5StarsQuestionEvaluationHandler : QuestionEvaluationHandler
    {
        private readonly IMapper mapper;
        private readonly IPercentCorrectionService correctionService;

        public Rate1To5StarsQuestionEvaluationHandler(IMapper mapper, IPercentCorrectionService correctionService)
        {
            this.mapper = mapper;
            this.correctionService = correctionService;
        }

        protected override Task<bool> CanHandleAsync(Question question) =>
         Task.FromResult(question.QuestionType == QuestionType.Rate1to5Stars);


        protected override async Task<BaseQuestionEvaluationModel> HandleAsync(Question question)
        {
            var result = this.mapper.Map<Rate1to5StarsQuestionEvaluationModel>(question);
            var keys = result.DistributionOfResults.Select(x => x.Key).ToArray();

            foreach (var key in keys)
            {
                var intKey = GlobalConstants.Rate1To5StarsValues[key];

                var answersCount = question.Answers.OfType<Rate1To5StarsAnswer>().Count(a => a.Value == intKey);
                decimal realValue = 0m;
                if (result.AnswersCount > 0)
                {
                    realValue = ((decimal)answersCount / result.AnswersCount) * 100m;
                }

                result.DistributionOfResults[key] = new DistributionOfResultsModel(answersCount, Math.Round(realValue));
            }
            this.correctionService.CorrectResult(result.DistributionOfResults);
            return await Task.FromResult(result);
        }
    }
}