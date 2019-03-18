using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Queries.GetNextQuestionDisplayNumber
{
    using FluentValidation;
    using Resources;

    class GetNextQuestionDisplayNumberQueryValidator:AbstractValidator<GetNextQuestionDisplayNumberQuery>
    {
        public GetNextQuestionDisplayNumberQueryValidator()
        {
            RuleFor(x => x.SurveyId).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
