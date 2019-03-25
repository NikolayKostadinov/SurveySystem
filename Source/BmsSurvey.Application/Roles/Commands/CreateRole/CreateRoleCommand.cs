//  ------------------------------------------------------------------------------------------------
//   <copyright file="CreateRoleCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Commands.CreateRole
{
    #region Using

    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;
    using MediatR;

    #endregion

    public class CreateRoleCommand : IRequest
    {
        [Display(Name = "NAME")]
        public string Name { get; set; }

        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }
    }
}