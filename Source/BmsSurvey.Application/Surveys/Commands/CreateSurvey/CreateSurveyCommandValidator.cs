using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.CreateSurvey
{
    using Common.Abstract;
    using FluentValidation;
    using Resources;

    public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyCommand>
    {
        public CreateSurveyCommandValidator()
        {
            RuleFor(x => x.SurveyTitle).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(MessageResource.GREATER_THAN)
                .NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.ActiveFrom).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.ActiveTo).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.ActiveTo).Must((x, activeTo) => x.ActiveFrom < activeTo)
                .WithMessage(string.Format(MessageResource.ACTIVE_TO_GREATER_THAN_ACTIVE_FROM));
        }
    }
}
