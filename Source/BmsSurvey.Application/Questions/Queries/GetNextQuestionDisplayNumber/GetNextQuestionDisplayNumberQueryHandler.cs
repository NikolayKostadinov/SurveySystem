using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Questions.Queries.GetNextQuestionDisplayNumber
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public class GetNextQuestionDisplayNumberQueryHandler : IRequestHandler<GetNextQuestionDisplayNumberQuery, int>
    {
        private readonly BmsSurveyDbContext context;

        public GetNextQuestionDisplayNumberQueryHandler(BmsSurveyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<int> Handle(GetNextQuestionDisplayNumberQuery request, CancellationToken cancellationToken)
        {
            var survey = this.context.Surveys.Include(s => s.Questions).FirstOrDefault(x => x.Id == request.SurveyId);
            if (survey == null)
            {
                throw new NotFoundException(nameof(survey), request.SurveyId);
            }

            var maxDisplayNumber = survey.Questions
                .Where(x => !x.IsDeleted)
                .Max(x => x.DisplayNumber);

            return Task.FromResult(maxDisplayNumber + 1);
        }
    }
}
