//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetAllUsersQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Queries.GetAllUsers
{
    #region Using

    using System.Collections.Generic;
    using MediatR;
    using Models;

    #endregion

    public class GetAllUsersQuery : IRequest<IEnumerable<UserListViewModel>>
    {
    }
}