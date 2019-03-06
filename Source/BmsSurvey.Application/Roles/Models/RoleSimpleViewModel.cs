//  ------------------------------------------------------------------------------------------------
//   <copyright file="RoleSimpleViewModel.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Models
{
    #region Using

    using Domain.Entities.Identity;
    using Interfaces.Mapping;

    #endregion

    public class RoleSimpleViewModel : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}