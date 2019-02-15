using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Models
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;
    using Questions.Models;

    public class SurveyViewModel:IMapFrom<Survey>,IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public bool IsLastPage { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Survey, SurveyViewModel>()
                .ForMember(s => s.Questions, opt => opt.Ignore());
        }
    }
}
