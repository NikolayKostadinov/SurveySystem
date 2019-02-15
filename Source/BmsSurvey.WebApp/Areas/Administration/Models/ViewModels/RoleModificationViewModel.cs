namespace BmsSurvey.WebApp.Areas.Administration.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RoleModificationViewModel
    {
        [Required]
        public string RoleName { get; set; }

        [Required]
        [Display(Name="RoleName")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; }

        public int[] IdsToAdd { get; set; }

        public int[]IdsToDelete { get; set; }
    }
}