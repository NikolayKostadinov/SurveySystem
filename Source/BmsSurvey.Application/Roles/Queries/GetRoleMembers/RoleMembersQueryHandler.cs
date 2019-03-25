using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetRoleMembers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using Exceptions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Users.Models;

    public class RoleMembersQueryHandler:IRequestHandler<RoleMembersQuery, IEnumerable<UserSimpleViewModel>>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public RoleMembersQueryHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserSimpleViewModel>> Handle(RoleMembersQuery request, CancellationToken cancellationToken)
        {
            var role = await this.roleManager.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r=>r.Name == request.Name, cancellationToken);

            if (role is null)
            {
                throw new NotFoundException(nameof(role), request.Name);
            }

            return this.mapper.Map<IEnumerable<UserSimpleViewModel>>(role.UserRoles.Select(ur => ur.User));
        }
    }
}
