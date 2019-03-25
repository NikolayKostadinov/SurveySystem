//  ------------------------------------------------------------------------------------------------
//   <copyright file="RolePresentationViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Models
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;

    #endregion

    public class RolePresentationViewModel : IMapFrom<Role>, IHaveCustomMapping
    {
        [Required] public int Id { get; set; }

        [Display(Name = "NAME")] public string Name { get; set; }

        [Display(Name = "DESCRIPTION")] public string Description { get; set; }

        [Display(Name = "USERS")] public IEnumerable<string> Users { get; set; }

        /// <summary>
        ///     Creates the mappings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RolePresentationViewModel, Role>()
                .ForMember(p => p.UserRoles, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.DeletedOn, opt => opt.Ignore())
                .ForMember(p => p.DeletedFrom, opt => opt.Ignore())
                .ForMember(p => p.CreatedOn, opt => opt.Ignore())
                .ForMember(p => p.PreserveCreatedOn, opt => opt.Ignore())
                .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
                .ForMember(p => p.CreatedFrom, opt => opt.Ignore())
                .ForMember(p => p.ModifiedFrom, opt => opt.Ignore());
            configuration.CreateMap<Role, RolePresentationViewModel>()
                .ForMember(p => p.Users, opt => opt.MapFrom(p => p.UserRoles.Select(ur => ur.User).Where(u => !u.IsDeleted).ToList()));
        }
    }
}