namespace BmsSurvey.Application.Questions.Commands.EditQuestion
{
    using System;
    using CreateQuestion;
    using Domain.Entities;
    using FluentValidation;
    using Resources;
    using Surveys.Commands.CreateSurvey;

    public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
    {
        public EditQuestionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.Text).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.QuestionType).Must(x => Enum.IsDefined(typeof(QuestionType), x)).WithMessage(MessageResource.REQUIRED);
        }
    }
}
