namespace BmsSurvey.Application.Questions.Commands.EditQuestion
{
    using CreateQuestion;
    using FluentValidation;
    using Resources;
    using Surveys.Commands.CreateSurvey;

    public class EditQueryCommandValidator : AbstractValidator<EditQuestionCommand>
    {
        
        public EditQueryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.SurveyId).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.Text).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.QuestionType).NotEmpty().WithMessage(MessageResource.REQUIRED);
        }
    }
}
