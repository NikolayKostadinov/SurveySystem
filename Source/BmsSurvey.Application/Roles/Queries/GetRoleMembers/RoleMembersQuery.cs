//  ------------------------------------------------------------------------------------------------
//   <copyright file="RoleMembersQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Queries.GetRoleMembers
{
    #region Using

    using System.Collections.Generic;
    using MediatR;
    using Users.Models;

    #endregion

    public class RoleMembersQuery : IRequest<IEnumerable<UserSimpleViewModel>>
    {
        public string Name { get; set; }
    }
}