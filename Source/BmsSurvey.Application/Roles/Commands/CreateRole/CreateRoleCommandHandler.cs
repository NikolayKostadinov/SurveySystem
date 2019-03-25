using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.CreateRole
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities.Identity;
    using Infrastructure.Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand>
    {
        private readonly RoleManager<Role> roleManager;

        public CreateRoleCommandHandler(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var identityResult = await roleManager.CreateAsync(new Role(request.Name, request.Description));
            identityResult.Check(nameof(CreateRoleCommand));
            return await Unit.Task;
        }
    }
}
