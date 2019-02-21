namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestionsWithAnswers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Extensions;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;
    using Questions.Models;

    public class SurveyQuestionsQueryHandler : IRequestHandler<SurveyQuestionsQuery, SurveyViewModel>
    {
        private readonly BmsSurveyDbContext context;
        private readonly ISurveyDto surveyDto;
        private readonly IMapper mapper;

        public SurveyQuestionsQueryHandler(BmsSurveyDbContext context, ISurveyDto surveyDto, IMapper mapper)
        {
            this.context = context;
            this.surveyDto = surveyDto;
            this.mapper = mapper;
        }

        public async Task<SurveyViewModel> Handle(SurveyQuestionsQuery request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys.Include(s => s.Questions)
                .Include(x => x.CompletedSurveys)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (survey is null || !survey.IsActive)
            {
                throw new NotFoundException(nameof(survey), request.Id);
            }

            if (survey.CompletedSurveys.Any(x => x.IpAddress == request.IpAddress))
            {
                throw new SurveyCompletedException(survey.Id, request.IpAddress);
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

            questions.ForEach(q =>
            {
                if (answers.TryGetValue(q.Id, out var answer))
                {
                    q.Value = answer.Value;
                    q.IsChecked = true;
                }
            });

            var result = this.mapper.Map<SurveyViewModel>(survey);

            result.PageNumber = request.PageNumber;
            result.IsLastPage = survey.Questions.Count() <= request.PageNumber * survey.PageSize;
            result.Questions = questions;

            return result;
        }
    }
}
