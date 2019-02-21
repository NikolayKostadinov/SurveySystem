﻿//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateSurveyCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Surveys.Commands.EditSurvey
{
    #region Using

    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CreateSurvey;
    using Domain.Entities;
    using Interfaces.Mapping;
    using MediatR;

    #endregion

    public class EditSurveyCommand : CreateSurveyCommand
    {
        public int Id { get; set; }

        public override void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<EditSurveyCommand, Survey>()
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
        //[Display(Name = "TITLE")]
        //public string Title { get; set; }

        //[Display(Name = "DESCRIPTION")]
        //public string Description { get; set; }

        //[Display(Name = "PAGE_SIZE")]
        //public int PageSize { get; set; }

        //[Display(Name = "ACTIVE_FROM")]
        //public DateTime ActiveFrom { get; set; }

        //[Display(Name = "ACTIVE_TO")]
        //public DateTime ActiveTo { get; set; }

        //public void CreateMappings(Profile configuration)
        //{
        //    configuration.CreateMap<EditSurveyCommand, Survey>()
        //        .ForMember(p => p.Id, opt => opt.Ignore())
        //        .ForMember(p => p.Questions, opt => opt.Ignore())
        //        .ForMember(p => p.IsDeleted, opt => opt.Ignore())
        //        .ForMember(p => p.DeletedOn, opt => opt.Ignore())
        //        .ForMember(p => p.DeletedFrom, opt => opt.Ignore())
        //        .ForMember(p => p.CreatedOn, opt => opt.Ignore())
        //        .ForMember(p => p.PreserveCreatedOn, opt => opt.Ignore())
        //        .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
        //        .ForMember(p => p.CreatedFrom, opt => opt.Ignore())
        //        .ForMember(p => p.ModifiedFrom, opt => opt.Ignore());
        //}
    }
}