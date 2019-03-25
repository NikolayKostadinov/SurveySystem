using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetAllRolesPresentation
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class RolesPresentationQueryHandler:IRequestHandler<RolesPresentationQuery, IEnumerable<RolePresentationViewModel>>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public RolesPresentationQueryHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RolePresentationViewModel>> Handle(RolesPresentationQuery request, CancellationToken cancellationToken)
        {
            var roles = await roleManager.Roles
                .Include(r=>r.UserRoles)
                .ThenInclude(x=>x.User)
                .Where(r=>!r.IsDeleted).ToListAsync(cancellationToken);
            return this.mapper.Map<IEnumerable<RolePresentationViewModel>>(roles);
        }
    }
}
