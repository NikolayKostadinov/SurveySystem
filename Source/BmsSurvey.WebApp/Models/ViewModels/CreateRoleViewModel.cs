namespace BmsSurvey.WebApp.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.Mapping;
    using AutoMapper;
    using Domain.Entities.Identity;

    public class CreateRoleViewModel:IMapFrom<Role>,IHaveCustomMapping
    {
        [Required]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Достъпна за администратори")]
        public bool IsAvailableForAdministrators { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateRoleViewModel, Role>()
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