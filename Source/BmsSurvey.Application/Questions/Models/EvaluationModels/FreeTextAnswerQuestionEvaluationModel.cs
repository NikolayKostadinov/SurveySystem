using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Models.EvaluationModels
{
    using System.Linq;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Entities.Answers;

    public class FreeTextAnswerQuestionEvaluationModel : BaseQuestionEvaluationModel
    {
        protected override Dictionary<string, DistributionOfResultsModel> GetValues() => new Dictionary<string, DistributionOfResultsModel>();
        public IEnumerable<FreeAnswerViewModel> Answers { get; set; }

        public FreeTextAnswerQuestionEvaluationModel()
        {
            Answers = new List<FreeAnswerViewModel>();
        }

        public override void CreateMappings(Profile configuration)
        {
            base.CreateMappings(configuration);
            configuration.CreateMap<Question, FreeTextAnswerQuestionEvaluationModel>()
                .ForMember(x => x.Answers,
                    opt => opt.Ignore());
        }
    }
}
