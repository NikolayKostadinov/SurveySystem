namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestionsWithAnswers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Extensions;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Questions.Models;

    public class SurveyQuestionsQueryHandler : IRequestHandler<SurveyQuestionsQuery, SurveyViewModel>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly ISurveyDto surveyDto;
        private readonly IMapper mapper;

        public SurveyQuestionsQueryHandler(IBmsSurveyDbContext context, ISurveyDto surveyDto, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.surveyDto = surveyDto ?? throw new ArgumentNullException(nameof(surveyDto));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SurveyViewModel> Handle(SurveyQuestionsQuery request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys.Include(s => s.Questions)
                .Include(x => x.CompletedSurveys)
                .FirstOrDefaultAsync(x => x.Id == request.Id
                                          && x.IsDeleted == false
                    , cancellationToken);
            if (survey is null || !survey.IsActive)
            {
                throw new NotFoundException(nameof(survey), request.Id);
            }

            var questions = survey.Questions
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayNumber)
                .Skip(survey.PageSize * (request.PageNumber - 1))
                .Take(survey.PageSize)
                .Select(x => new QuestionViewModel()
                {
                    Id = x.Id,
                    QuestionType = x.QuestionType,
                    Text = x.Text,
                    DisplayNumber = x.DisplayNumber,
                })
                .ToArray();

            var answers = surveyDto.Answers;
            if (answers?.Any() ?? false)
            {
                questions.ForEach(q =>
                {
                    if (answers.TryGetValue(q.Id, out var answer))
                    {
                        q.Value = answer.Value;
                        q.IsChecked = true;
                    }
                });
            }

            var result = this.mapper.Map<SurveyViewModel>(survey);

            result.PageNumber = request.PageNumber;
            result.IsLastPage = survey.Questions.Count() <= request.PageNumber * survey.PageSize;
            result.Questions = questions;

            return result;
        }
    }
}
