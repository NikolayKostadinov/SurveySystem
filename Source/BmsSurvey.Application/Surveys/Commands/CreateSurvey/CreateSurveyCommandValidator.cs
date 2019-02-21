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
            RuleFor(x => x.Title).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(string.Format(MessageResource.GREATER_THAN, 0));
            RuleFor(x => x.ActiveFrom).NotEmpty().GreaterThanOrEqualTo(TimeProvider.Current.UtcNow.Date)
                .WithMessage(string.Format(MessageResource.GREATER_THAN, TimeProvider.Current.UtcNow));
            RuleFor(x => x.ActiveTo).NotEmpty().GreaterThanOrEqualTo(TimeProvider.Current.UtcNow.Date)
                .WithMessage(string.Format(MessageResource.GREATER_THAN, TimeProvider.Current.UtcNow.Date));
            RuleFor(x => x.ActiveTo).Must((x, activeTo) => x.ActiveFrom < activeTo)
                .WithMessage(string.Format(MessageResource.ACTIVE_TO_GREATER_THAN_ACTIVE_FROM));
        }
    }
}
