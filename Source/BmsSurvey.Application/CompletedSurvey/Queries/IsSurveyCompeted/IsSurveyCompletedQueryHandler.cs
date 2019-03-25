namespace BmsSurvey.Application.CompletedSurvey.Queries.IsSurveyCompeted
{
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class IsSurveyCompletedQueryHandler:IRequestHandler<IsSurveyCompletedQuery,bool>
    {
        private readonly IBmsSurveyDbContext context;

        public IsSurveyCompletedQueryHandler(IBmsSurveyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> Handle(IsSurveyCompletedQuery request, CancellationToken cancellationToken)
        {
            return this.context.CompletedSurveys.AnyAsync(cs=>cs.IsDeleted==false 
                                                              && cs.SurveyId == request.SurveyId
                                                              && cs.IpAddress == request.IpAddress);
        }
    }
}
