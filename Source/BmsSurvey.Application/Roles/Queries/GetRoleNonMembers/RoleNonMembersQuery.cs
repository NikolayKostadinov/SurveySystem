//  ------------------------------------------------------------------------------------------------
//   <copyright file="RoleNonMembersQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Roles.Queries.GetRoleNonMembers
{
    #region Using

    using System.Collections.Generic;
    using MediatR;
    using Users.Models;

    #endregion

    public class RoleNonMembersQuery : IRequest<IEnumerable<UserSimpleViewModel>>
    {
        public string Name { get; set; }
    }
}