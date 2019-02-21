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
    using MediatR;
    using Surveys.Commands.CreateSurvey;

    #endregion

    public class EditQuestionCommand : CreateQuestionCommand
    {
        public int Id { get; set; }

        public override void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EditQuestionCommand, Survey>()
               .ForMember(p => p.Questions, opt => opt.Ignore())
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