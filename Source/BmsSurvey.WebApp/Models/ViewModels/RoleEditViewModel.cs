namespace BmsSurvey.WebApp.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces.Mapping;
    using Domain.Entities.Identity;

    public class RoleEditViewModel
    {
       
        public Role Role { get; set; }

        [Required(ErrorMessage = "REQUIRED")]
        [Display(Name="RoleName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name="Description")]
        public string Description { get; set; }

        public IEnumerable<UserSimpleViewModel> Members { get; set; }
        public IEnumerable<UserSimpleViewModel> NonMembers { get; set; }
    }
 
    /// <summary>
    /// 
    /// </summary>
    public class UserSimpleViewModel:IMapFrom<User>
    {
        [Required]
        public int Id { get; set; }
        [Display(Name="USERNAME")]
        public string UserName { get; set; }
        [Display(Name="FULLNAME")]
        public string FullName { get; set; }
    }
}