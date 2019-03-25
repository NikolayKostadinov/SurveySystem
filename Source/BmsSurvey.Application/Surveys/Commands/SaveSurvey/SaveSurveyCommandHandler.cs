using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.SaveSurvey
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Abstract;
    using Domain.Entities;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SaveSurveyCommandHandler : IRequestHandler<SaveSurveyCommand>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly ISurveyDto surveyDto;
        private readonly IAnswerFactory answerFactory;

        public SaveSurveyCommandHandler(IBmsSurveyDbContext context,
            ISurveyDto surveyDto,
            IAnswerFactory answerFactory)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.surveyDto = surveyDto ?? throw new ArgumentNullException(nameof(surveyDto));
            this.answerFactory = answerFactory ?? throw new ArgumentNullException(nameof(answerFactory));
        }


        public async Task<Unit> Handle(SaveSurveyCommand request, CancellationToken cancellationToken)
        {
            if (surveyDto.Answers.Any())
            {
                var survey = await this.context.Surveys
                    .Include(s => s.Questions)
                    .FirstOrDefaultAsync(s => s.Id == request.Id && s.IsDeleted == false, cancellationToken);
                if (survey is null || !survey.IsActive)
                {
                    throw new NotFoundException(nameof(survey), request.Id);
                }

                var question = survey.Questions.ToDictionary(q => q.Id);

                var answers = surveyDto.Answers.Values.Select(a =>
                    this.answerFactory.GetAnswer(question[a.QuestionId].QuestionType, a));

                this.context.Answers.AddRange(answers);
                this.context.CompletedSurveys.Add(
                    new CompletedSurvey
                    {
                        SurveyId = survey.Id,
                        IpAddress = request.IpAddress,
                        Email = request.EMail
                    });
                await this.context.SaveChangesAsync(request.UserName, cancellationToken);

                surveyDto.ClearAnswers();
            }

            return Unit.Value;
        }
    }
}
