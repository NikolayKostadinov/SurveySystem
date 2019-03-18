using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Models.EvaluationModels
{
    using System.Linq;
    using AutoMapper;
    using Common.Constants;
    using Domain.Entities;
    using Domain.Entities.Answers;

    public class Rate1to5StarsQuestionEvaluationModel : BaseQuestionEvaluationModel
    {
        public decimal AverageValue { get; set; }
        protected override Dictionary<string, DistributionOfResultsModel> GetValues() =>
           GlobalConstants.Rate1To5StarsValues.Select(x => x.Key).ToDictionary(r => r, r => new DistributionOfResultsModel(0, 0m));

        public override void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Question, Rate1to5StarsQuestionEvaluationModel>()
                .ForMember(p => p.AverageValue, opt => opt.MapFrom(p =>
                        Math.Round(p.Answers.Any() ? p.Answers.OfType<Rate1To5StarsAnswer>().Average(a => a.Value) : 0, 2)
                      ));
        }
    }
}
