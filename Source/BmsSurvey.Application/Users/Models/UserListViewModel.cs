﻿//  ------------------------------------------------------------------------------------------------
//   <copyright file="UserListViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Models
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;

    #endregion

    public class UserListViewModel : IMapFrom<User>, IHaveCustomMapping
    {
        public int Id { get; set; }

        [Display(Name = "IsLocked")] public bool IsLocked { get; set; }

        [Display(Name = "TABNUMBER")] public string TabNumber { get; set; }

        [Display(Name = "USERNAME")] public string UserName { get; set; }

        [Display(Name = "FIRSTNAME")] public string FirstName { get; set; }

        [Display(Name = "SIRNAME")] public string SirName { get; set; }

        [Display(Name = "LASTNAME")] public string LastName { get; set; }

        [Display(Name = "FULLNAME")] public string FullName { get; set; }

        [UIHint("UserRoleEditor")]
        [Display(Name = "ROLES")]
        public ICollection<RoleSimpleViewModel> Roles { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, UserListViewModel>()
                .ForMember(p => p.IsLocked, opt => opt.MapFrom(p => p.LockoutEnd != null))
                .ForMember(p => p.Roles, opt => opt.MapFrom(p => p.UserRoles.Select(x => x.Role).ToList()));

            configuration.CreateMap<UserListViewModel, User>()
                .ForMember(p => p.UserRoles,
                    opt => opt.MapFrom(p => p.Roles.Select(x => new UserRole {UserId = p.Id, RoleId = x.Id}).ToList()));
        }
    }
}