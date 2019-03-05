using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Queries.GetAllSurveys
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;

    public class AllSurveysQueryHandler : IRequestHandler<AllSurveysQuery, IEnumerable<SurveyListRowViewModel>>
    {
        private readonly BmsSurveyDbContext context;
        private readonly IMapper mapper;

        public AllSurveysQueryHandler(BmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SurveyListRowViewModel>> Handle(AllSurveysQuery request, CancellationToken cancellationToken)
        {
            var dbSurveys = await this.context.Surveys.Where(x => x.IsDeleted == false).ToArrayAsync(cancellationToken);
            var surveys = this.mapper.Map<IEnumerable<SurveyListRowViewModel>>(dbSurveys);

            return surveys.ToList();
        }
    }
}
