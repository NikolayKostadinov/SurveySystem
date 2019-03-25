using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.DeleteRole
{
    using MediatR;

    public class DeleteRoleCommand:IRequest
    {
        public int Id { get; set; }
    }
}
