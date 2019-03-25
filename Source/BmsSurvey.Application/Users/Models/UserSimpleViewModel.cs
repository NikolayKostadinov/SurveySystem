using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;

    public class UserSimpleViewModel : IMapFrom<User>
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "USERNAME")]
        public string UserName { get; set; }

        [Display(Name = "FULLNAME")]
        public string FullName { get; set; }
    }
}
