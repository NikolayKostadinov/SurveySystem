using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetAllRolesDropdownData
{
    using MediatR;
    using Models;

    public class GetAllRolesDropdownDataQuery:IRequest<IEnumerable<RoleSimpleViewModel>>, IRequest<Unit>
    {
    }
}
