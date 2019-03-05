//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateSurveyCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.EditQuestion
{
    #region Using

    using AutoMapper;
    using CreateQuestion;
    using Domain.Entities;
    using Interfaces.Mapping;
    using MediatR;
    using Models;
    using Surveys.Commands.CreateSurvey;

    #endregion

    public class EditQuestionCommand:IRequest<QuestionListViewModel>, IMapFrom<Question>, IHaveCustomMapping
    { 
        public int Id { get; set; }

        public int DisplayNumber { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EditQuestionCommand, Question>()
                .ForMember(p => p.SurveyId, opt => opt.Ignore())
                .ForMember(p => p.Answers, opt => opt.Ignore())
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