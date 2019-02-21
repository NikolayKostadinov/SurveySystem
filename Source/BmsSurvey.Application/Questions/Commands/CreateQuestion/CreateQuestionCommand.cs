//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateSurveyCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.CreateQuestion
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;
    using MediatR;
    using Models;

    #endregion

    public class CreateQuestionCommand : IRequest<QuestionListViewModel>, IMapFrom<Question>, IHaveCustomMapping
    {
        [Display(Name = "SURVEY_ID")]
        public int SurveyId { get; set; }

        [Display(Name = "DISPLAY_NUMBER")]
        public int DisplayNumber { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "QUESTION_TYPE")]
        public QuestionType QuestionType{ get; set; }

        public virtual void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateQuestionCommand, Question>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.DeletedOn, opt => opt.Ignore())
                .ForMember(p => p.DeletedFrom, opt => opt.Ignore())
                .ForMember(p => p.CreatedOn, opt => opt.Ignore())
                .ForMember(p => p.PreserveCreatedOn, opt => opt.Ignore())
                .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
                .ForMember(p => p.CreatedFrom, opt => opt.Ignore())
                .ForMember(p => p.ModifiedFrom, opt => opt.Ignore());
        }
    }
}