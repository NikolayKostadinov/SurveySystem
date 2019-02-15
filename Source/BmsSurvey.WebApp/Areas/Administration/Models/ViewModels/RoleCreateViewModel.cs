namespace BmsSurvey.WebApp.Areas.Administration.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.Mapping;
    using AutoMapper;
    using Domain.Entities.Identity;

    public class RoleCreateViewModel : IMapFrom<Role>,IHaveCustomMapping
    {
        [Required]
        [Display(Name="RoleName")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; }

        /// <summary>
        /// Creates the mappings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RoleCreateViewModel, Role>()
                .ForMember(p => p.UserRoles, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.DeletedOn, opt => opt.Ignore())
                .ForMember(p => p.DeletedFrom, opt => opt.Ignore())
                .ForMember(p => p.CreatedOn, opt => opt.Ignore())
                .ForMember(p => p.PreserveCreatedOn, opt => opt.Ignore())
                .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
                .ForMember(p => p.CreatedFrom, opt => opt.Ignore())
                .ForMember(p => p.ModifiedFrom, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}