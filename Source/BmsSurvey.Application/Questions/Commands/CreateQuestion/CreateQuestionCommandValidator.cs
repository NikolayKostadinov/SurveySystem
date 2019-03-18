namespace BmsSurvey.Application.Questions.Commands.CreateQuestion
{
    using System;
    using System.Linq;
    using Common.Abstract;
    using Domain.Entities;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using Resources;

    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator(BmsSurveyDbContext context)
        {
            RuleFor(x => x.SurveyId).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.Text).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber)
                .Must((x, displayNumber) =>
                    !context.Surveys
                        .Include(q => q.Questions)
                        .FirstOrDefault(q => q.Id == x.SurveyId)
                        ?.Questions.Any(y => y.DisplayNumber == displayNumber
                                                    && y.IsDeleted == false)
                    ?? false).WithMessage(MessageResource.UNICK);
            RuleFor(x => x.QuestionType).Must(x => Enum.IsDefined(typeof(QuestionType), x)).WithMessage(MessageResource.REQUIRED);
        }
    }
}
