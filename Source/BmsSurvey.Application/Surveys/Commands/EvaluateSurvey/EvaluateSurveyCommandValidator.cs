using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.EvaluateSurvey
{
    using FluentValidation;
    using Resources;

    class EvaluateSurveyCommandValidator:AbstractValidator<EvaluateSurveyCommand>
    {
        public EvaluateSurveyCommandValidator()
        {
            RuleFor(esc => esc.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
