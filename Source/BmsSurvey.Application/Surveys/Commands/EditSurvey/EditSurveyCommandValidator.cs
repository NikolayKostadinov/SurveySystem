namespace BmsSurvey.Application.Surveys.Commands.EditSurvey
{
    using Common.Abstract;
    using CreateSurvey;
    using FluentValidation;
    using Resources;

    public class EditSurveyCommandValidator : AbstractValidator<EditSurveyCommand>
    {
        public EditSurveyCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.SurveyTitle).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(string.Format(MessageResource.GREATER_THAN, 0));
            RuleFor(x => x.ActiveFrom).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.ActiveTo).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.ActiveTo).Must((x, activeTo) => x.ActiveFrom < activeTo)
                .WithMessage(string.Format(MessageResource.ACTIVE_TO_GREATER_THAN_ACTIVE_FROM));
        }
    }
}
