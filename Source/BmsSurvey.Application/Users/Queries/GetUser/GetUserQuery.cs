//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetUserQuery.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Queries.GetUser
{
    #region Using

    using System;
    using System.Security.Principal;
    using Domain.Entities.Identity;
    using MediatR;

    #endregion

    public class GetUserQuery : IRequest<User>
    {
        public GetUserQuery(IPrincipal user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public IPrincipal User { get; }
    }
}