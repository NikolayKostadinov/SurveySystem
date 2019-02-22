namespace BmsSurvey.Application.CompletedSurvey.Queries.IsSurveyCompeted
{
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public class IsSurveyCompletedQueryHandler:IRequestHandler<IsSurveyCompletedQuery,bool>
    {
        private readonly BmsSurveyDbContext context;

        public IsSurveyCompletedQueryHandler(BmsSurveyDbContext context)
        {
            this.context = context;
        }

        public Task<bool> Handle(IsSurveyCompletedQuery request, CancellationToken cancellationToken)
        {
            return this.context.CompletedSurveys.AnyAsync(cs=>cs.IsDeleted==false 
                                                              && cs.SurveyId == request.SurveyId
                                                              && cs.IpAddress == request.IpAddress);
        }
    }
}
