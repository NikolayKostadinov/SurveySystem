using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Queries.GetSurveyQuestions
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;
    using Persistence.Interfaces;
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
            var answers = surveyDto.Questions;
            var survey = await this.context.Surveys.Include(s=>s.Questions).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (survey is null || !survey.IsActive)
            {
                throw new NotFoundException(nameof(survey), request.Id);
            }

            var questions = await this.context.Questions
                .Where(x => !x.IsDeleted && x.SurveyId == request.Id)
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
                .ToListAsync(cancellationToken);


            questions.ForEach(q =>
            {
                if (answers.TryGetValue(q.Id, out var answer))
                {
                    q.Value = answer.Value;
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
