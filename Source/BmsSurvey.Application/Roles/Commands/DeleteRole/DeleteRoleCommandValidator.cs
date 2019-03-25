using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.DeleteRole
{
    using FluentValidation;
    using Resources;

    public class DeleteRoleCommandValidator:AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(rd => rd.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
