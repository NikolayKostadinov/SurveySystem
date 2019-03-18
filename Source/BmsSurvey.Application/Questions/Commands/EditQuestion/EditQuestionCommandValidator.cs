namespace BmsSurvey.Application.Questions.Commands.EditQuestion
{
    using System;
    using System.Linq;
    using CreateQuestion;
    using Domain.Entities;
    using Exceptions;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using Resources;
    using Surveys.Commands.CreateSurvey;

    public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
    {

        public EditQuestionCommandValidator(BmsSurveyDbContext context)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.Text).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber).NotEmpty().WithMessage(MessageResource.REQUIRED);
            RuleFor(x => x.DisplayNumber)
                .Must((x, displayNumber) =>
                 !context.Questions
                        .Include(q => q.Survey)
                        .ThenInclude(qs => qs.Questions)
                        .FirstOrDefault(q => q.Id == x.Id)
                        ?.Survey.Questions.Any(y => y.Id != x.Id 
                                                    && y.DisplayNumber == displayNumber
                                                    && y.IsDeleted == false) 
                 ?? false).WithMessage(MessageResource.UNICK);

            RuleFor(x => x.QuestionType).Must(x => Enum.IsDefined(typeof(QuestionType), x)).WithMessage(MessageResource.REQUIRED);
        }
    }
}
