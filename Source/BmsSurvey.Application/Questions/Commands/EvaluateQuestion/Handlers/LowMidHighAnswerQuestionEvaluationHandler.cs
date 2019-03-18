﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Constants;
    using Domain.Entities;
    using Domain.Entities.Answers;
    using Models.EvaluationModels;

    public class LowMidHighAnswerQuestionEvaluationHandler:QuestionEvaluationHandler
    {
        private readonly IMapper mapper;
        private readonly IPercentCorrectionService correctionService;

        public LowMidHighAnswerQuestionEvaluationHandler(IMapper mapper, IPercentCorrectionService correctionService)
        {
            this.mapper = mapper;
            this.correctionService = correctionService;
        }

        protected override Task<bool> CanHandleAsync(Question question) =>
            Task.FromResult(question.QuestionType == QuestionType.LowMidHigh);


        protected override async Task<BaseQuestionEvaluationModel> HandleAsync(Question question)
        {
            var result = this.mapper.Map<LowMidHighAnswerQuestionEvaluationModel>(question);
            var keys = result.DistributionOfResults.Select(x => x.Key).ToArray();
            
            foreach (var key in keys)
            {
                var intKey = GlobalConstants.LowMidHighValues[key];
                var answersCount = question.Answers.OfType<LowMidHighAnswer>().Count(a => a.Value == intKey);
                decimal realValue = 0m;
                if (result.AnswersCount > 0)
                {
                    realValue = ((decimal)answersCount / result.AnswersCount) * 100m;
                }
                result.DistributionOfResults[key]= new DistributionOfResultsModel(answersCount, Math.Round(realValue));
            }

            this.correctionService.CorrectResult(result.DistributionOfResults);
            return await Task.FromResult(result);
        }
    }
}
