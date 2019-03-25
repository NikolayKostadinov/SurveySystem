using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Roles.Commands.CreateRole
{
    using FluentValidation;
    using Resources;

    public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(cr=>cr.Name).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(cr=>cr.Description).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
