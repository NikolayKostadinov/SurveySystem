using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetRoleNonMembers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities.Identity;
    using GetRoleMembers;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Users.Models;

    public class RoleNonMembersQueryHandler : IRequestHandler<RoleNonMembersQuery, IEnumerable<UserSimpleViewModel>>
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RoleNonMembersQueryHandler(IMediator mediator, UserManager<User> userManager, IMapper mapper)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserSimpleViewModel>> Handle(RoleNonMembersQuery request, CancellationToken cancellationToken)
        {
            var includedUsers = await this.mediator.Send(new RoleMembersQuery() { Name = request.Name });
            var includedUserIds = includedUsers.Select(u => u.Id).ToArray();
            var nonIncludedUsers = await this.userManager.Users
                .Where(x => 
                    x.IsDeleted == false 
                    && !includedUserIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
            return this.mapper.Map<IEnumerable<UserSimpleViewModel>>(nonIncludedUsers);
        }
    }
}
