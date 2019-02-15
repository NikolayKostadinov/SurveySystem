namespace BmsSurvey.WebApp.Areas.Administration.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.Mapping;
    using AutoMapper;
    using Domain.Entities.Identity;

    public class RoleViewModel:IMapFrom<Role>, IHaveCustomMapping
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        /// <summary>
        /// Creates the mappings.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RoleViewModel, Role>()
                .ForMember(p => p.UserRoles, opt => opt.Ignore())
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