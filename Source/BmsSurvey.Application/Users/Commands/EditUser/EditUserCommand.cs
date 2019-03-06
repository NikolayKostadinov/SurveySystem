//  ------------------------------------------------------------------------------------------------
//   <copyright file="UpdateUserCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Commands.EditUser
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;
    using MediatR;
    using Models;
    using Roles.Models;

    #endregion

    public class EditUserCommand : IRequest<UserListViewModel>, IMapFrom<User>, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string TabNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SirName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<RoleSimpleViewModel> Roles { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, EditUserCommand>()
                .ForMember(p => p.Roles, opt => opt.MapFrom(p => p.UserRoles.Select(x => x.Role).ToList()));

            configuration.CreateMap<EditUserCommand, User>()
                .ForMember(p => p.UserRoles, opt => opt.Ignore())
                .ForMember(p => p.CreatedOn, opt => opt.Ignore())
                .ForMember(p => p.PreserveCreatedOn, opt => opt.Ignore())
                .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
                .ForMember(p => p.CreatedFrom, opt => opt.Ignore())
                .ForMember(p => p.ModifiedFrom, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.DeletedOn, opt => opt.Ignore())
                .ForMember(p => p.DeletedFrom, opt => opt.Ignore())
                .ForMember(p => p.UserName, opt => opt.Ignore())
                .ForMember(p => p.NormalizedUserName, opt => opt.Ignore())
                .ForMember(p => p.NormalizedEmail, opt => opt.Ignore())
                .ForMember(p => p.EmailConfirmed, opt => opt.Ignore())
                .ForMember(p => p.PasswordHash, opt => opt.Ignore())
                .ForMember(p => p.SecurityStamp, opt => opt.Ignore())
                .ForMember(p => p.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(p => p.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(p => p.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(p => p.LockoutEnd, opt => opt.Ignore())
                .ForMember(p => p.LockoutEnabled, opt => opt.Ignore())
                .ForMember(p => p.AccessFailedCount, opt => opt.Ignore());
        }
    }
}