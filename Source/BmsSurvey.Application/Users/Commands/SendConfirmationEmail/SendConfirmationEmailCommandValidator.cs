using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Users.Commands.SendConfirmationEmail
{
    using FluentValidation;
    using Resources;

    public class SendConfirmationEmailCommandValidator:AbstractValidator<SendConfirmationEmailCommand>
    {
        public SendConfirmationEmailCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
