using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetRoleForEditing
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
    using Models;
    using Users.Models;

    public class RoleForEditingQueryHandler : IRequestHandler<RoleForEditingQuery, RoleEditViewModel>
    {
        private readonly RoleManager<Role> roleManager;

        public RoleForEditingQueryHandler(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<RoleEditViewModel> Handle(RoleForEditingQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id ?? 0;
            var role = await this.roleManager.FindByIdAsync(id.ToString());

            return new RoleEditViewModel
            {
                Name = role.Name,
                RoleReferenceName = role.Name,
                Description = role.Description
            };
        }
    }
}
