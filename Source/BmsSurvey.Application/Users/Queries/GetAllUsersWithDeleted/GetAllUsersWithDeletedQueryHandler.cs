//  ------------------------------------------------------------------------------------------------
//   <copyright file="GetAllUsersQueryHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Users.Queries.GetAllUsersWithDeleted
{
    using System.Collections.Generic;
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
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserListViewModel>> Handle(GetAllUsersWithDeletedQuery request, CancellationToken cancellationToken)
        {
            var users = await this.userManager.Users
                .Include(usr => usr.UserRoles)
                .ThenInclude(usr => usr.Role)
                .ToListAsync(cancellationToken);

            var model = this.mapper.Map<IEnumerable<UserListViewModel>>(users);

            return model;

        }
    }
}