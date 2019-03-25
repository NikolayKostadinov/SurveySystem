using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.EditRole
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Exceptions;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;

        public EditRoleCommandHandler(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<Unit> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await this.roleManager.FindByNameAsync(request.RoleReferenceName);

            if (role is null)
            {
                throw new NotFoundException(nameof(role), request.Name);
            }

            role.Name = request.Name;
            role.Description = request.Description;
            var identityResult = await this.roleManager.UpdateAsync(role);

            identityResult.Check(nameof(EditRoleCommand));

            foreach (int userId in request.IdsToAdd ?? new int[] { })
            {
                var user = await this.userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                {
                    throw new NotFoundException(nameof(user), userId);
                }

                identityResult = await this.userManager.AddToRoleAsync(user, request.Name);
                identityResult.Check(nameof(EditRoleCommand));

                identityResult = await userManager.UpdateSecurityStampAsync(user);
                identityResult.Check(nameof(EditRoleCommand));
            }

            foreach (int userId in request.IdsToDelete ?? new int[] { })
            {
                var user = await this.userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                {
                    throw new NotFoundException(nameof(user), userId);
                }

                identityResult = await this.userManager.RemoveFromRoleAsync(user, request.Name);
                identityResult.Check(nameof(EditRoleCommand));

                identityResult = await userManager.UpdateSecurityStampAsync(user);
                identityResult.Check(nameof(EditRoleCommand));
            }

            return await Unit.Task;
        }
    }
}
