using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Models
{
    using System.ComponentModel.DataAnnotations;
    using Users.Models;

    public class RoleEditViewModel
    {
        public RoleEditViewModel()
        {
            Members = new List<UserSimpleViewModel>();
            NonMembers = new List<UserSimpleViewModel>();
        }

        [Display(Name = "NAME")]
        public string Name { get; set; }

        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }

        public string RoleReferenceName { get; set; }

        public IEnumerable<UserSimpleViewModel> Members { get; set; }
        public IEnumerable<UserSimpleViewModel> NonMembers { get; set; }
    }
}
