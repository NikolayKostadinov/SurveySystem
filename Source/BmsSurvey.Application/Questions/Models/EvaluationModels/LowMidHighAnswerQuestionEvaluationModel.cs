using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Models.EvaluationModels
{
    using System.Linq;
    using AutoMapper;
    using Common.Constants;
    using Domain.Entities;

    public class LowMidHighAnswerQuestionEvaluationModel: BaseQuestionEvaluationModel
    {
        protected override Dictionary<string, DistributionOfResultsModel> GetValues()=>
            GlobalConstants.LowMidHighValues.Select(x => x.Key).ToDictionary(r => r, r => new DistributionOfResultsModel(0,0m));

        public override void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Question, LowMidHighAnswerQuestionEvaluationModel>();
        }

    }
}
