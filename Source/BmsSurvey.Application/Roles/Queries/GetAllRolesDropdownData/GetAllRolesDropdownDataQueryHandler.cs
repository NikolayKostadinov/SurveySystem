using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetAllRolesDropdownData
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

    public class GetAllRolesDropdownDataQueryHandler: IRequestHandler<GetAllRolesDropdownDataQuery, IEnumerable<RoleSimpleViewModel>>
    {
        private readonly RoleManager<Role> roleManager;
        private readonly IMapper mapper;

        public GetAllRolesDropdownDataQueryHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public Task<IEnumerable<RoleSimpleViewModel>> Handle(GetAllRolesDropdownDataQuery request, CancellationToken cancellationToken)
        {
            var roles = this.roleManager.Roles.Where(x => x.IsDeleted == false).ToList();
            return Task.FromResult(this.mapper.Map<IEnumerable<RoleSimpleViewModel>>(roles));
        }
    }
}
