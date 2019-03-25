using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.DeleteRole
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await this.roleManager.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);


            if (role is null)
            {
                throw new NotFoundException(nameof(role), request.Id);
            }

            var users = role.UserRoles.Select(ur => ur.User).ToList();

            IdentityResult identityResult;

            foreach (var user in users)
            {
                identityResult = await userManager.RemoveFromRoleAsync(user, role.Name);
                identityResult.Check(nameof(DeleteRoleCommand));

                identityResult = await userManager.UpdateSecurityStampAsync(user);
                identityResult.Check(nameof(DeleteRoleCommandHandler));
            }

            identityResult = await this.roleManager.DeleteAsync(role);
            identityResult.Check(nameof(DeleteRoleCommand));

            return await Unit.Task;
        }
    }
}
