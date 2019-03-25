using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Queries.GetAllRolesPresentation
{
    using MediatR;
    using Models;

    public class RolesPresentationQuery: IRequest<IEnumerable<RolePresentationViewModel>>
    {

    }
}
