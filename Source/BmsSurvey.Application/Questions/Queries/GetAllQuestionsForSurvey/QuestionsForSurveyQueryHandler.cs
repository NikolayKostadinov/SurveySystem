using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Queries.GetAllQuestionsForSurvey
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class QuestionsForSurveyQueryHandler : IRequestHandler<QuestionsForSurveyQuery, IEnumerable<QuestionListViewModel>>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly IMapper mapper;

        public QuestionsForSurveyQueryHandler(IBmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<QuestionListViewModel>> Handle(QuestionsForSurveyQuery request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(
                    s => s.Id == request.SurveyId
                    && s.IsActive == true
                    && s.IsDeleted==false, cancellationToken);
            if (survey is null)
            {
                throw new NotFoundException(nameof(survey), request.SurveyId);
            }

            var dbQuestions = survey.Questions.Where(q => q.IsDeleted == false);
            var questions = this.mapper.Map<IEnumerable<QuestionListViewModel>>(dbQuestions);

            return questions;
        }
    }
}
