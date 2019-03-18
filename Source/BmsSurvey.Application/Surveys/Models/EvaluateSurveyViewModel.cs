//  ------------------------------------------------------------------------------------------------
//   <copyright file="EvaluateSurveyViewModel.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Surveys.Models
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;
    using Questions.Models.EvaluationModels;

    public class EvaluateSurveyViewModel:IMapFrom<Survey>,IHaveCustomMapping
    {
        public int Id { get; set; }

        public string SurveyTitle { get; set; }

        public string Description { get; set; }

        public DateTime ActiveFrom { get; set; }

        public DateTime ActiveTo { get; set; }

        public int CompletedSurveyCount { get; set; }

        public IEnumerable<BaseQuestionEvaluationModel> EvaluatedQuestions { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Survey, EvaluateSurveyViewModel>()
                .ForMember(p=>p.EvaluatedQuestions,opt=>opt.Ignore())
                .ForMember(p=>p.CompletedSurveyCount, opt=>opt.MapFrom(p=>p.CompletedSurveys.Count));
        }
    }
}