namespace BmsSurvey.Application.Questions.Commands.CreateQuestion
{
    using System;
    using Common.Abstract;
    using Domain.Entities;
    using FluentValidation;
    using Resources;

    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.SurveyId).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.Text).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.QuestionType).Must(x => Enum.IsDefined(typeof(QuestionType), x)).WithMessage(MessageResource.REQUIRED);
        }
    }
}
