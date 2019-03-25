//  ------------------------------------------------------------------------------------------------
//   <copyright file="EditRoleCommand.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Commands.EditRole
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities.Identity;
    using Interfaces.Mapping;
    using MediatR;
    using Models;
    using Users.Models;

    #endregion

    public class EditRoleCommand:IRequest<Unit>
    {

        [Display(Name = "RoleName")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public string RoleReferenceName { get; set; }

        public IEnumerable<int> IdsToAdd { get; set; }
        public IEnumerable<int> IdsToDelete { get; set; }
    }
}