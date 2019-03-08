//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetAllUsersQueryHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Queries.GetAllUsersWithDeleted
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class GetAllUsersWithDeletedQueryHandler : IRequestHandler<GetAllUsersWithDeletedQuery, IEnumerable<UserListViewModel>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GetAllUsersWithDeletedQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<UserListViewModel>> Handle(GetAllUsersWithDeletedQuery request, CancellationToken cancellationToken)
        {
            var users = this.userManager.Users
                .Include(usr => usr.UserRoles)
                .ThenInclude(usr => usr.Role)
                .ToList();

            var model = this.mapper.Map<IEnumerable<UserListViewModel>>(users);

            return Task.FromResult(model);

        }
    }
}