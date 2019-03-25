using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.EditRole
{
    using FluentValidation;
    using Resources;

    public class EditRoleCommandValidator:AbstractValidator<EditRoleCommand>
    {
        public EditRoleCommandValidator()
        {
            RuleFor(er => er.Name).NotNull().WithMessage(MessageResource.REQUIRED);
            RuleFor(er => er.Description).NotNull().WithMessage(MessageResource.REQUIRED);
            RuleFor(er => er.RoleReferenceName).NotNull().WithMessage(MessageResource.REQUIRED);
        }
    }
}
