//  ------------------------------------------------------------------------------------------------
//   <copyright file="QuestionEvaluationModel.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Questions.Models.EvaluationModels
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public abstract class BaseQuestionEvaluationModel : IMapFrom<Question>, IHaveCustomMapping
    {
        protected BaseQuestionEvaluationModel()
        {
            this.DistributionOfResults = this.GetValues();
        }

        public int AnswersCount { get; private set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int DisplayNumber { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.Rate1to5Stars;

        public Dictionary<string, DistributionOfResultsModel> DistributionOfResults { get; }
        public Dictionary<string, decimal> DistributionOfResultsPercentage => this.DistributionOfResults.Where(x => x.Value.Percentage > 0).Select(x => new { x.Key, x.Value.Percentage }).ToDictionary(x => x.Key, x => x.Percentage);
        public Dictionary<string, int> DistributionOfResultsAbsolute => this.DistributionOfResults.Select(x => new { x.Key, x.Value.AnswersCount }).ToDictionary(x => x.Key, x => x.AnswersCount);

        protected abstract Dictionary<string, DistributionOfResultsModel> GetValues();

        public virtual void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Question, BaseQuestionEvaluationModel>()
                .ForMember(p => p.AnswersCount, opt => opt.MapFrom(p => p.Answers.Count)).IncludeAllDerived();
        }
    }
}