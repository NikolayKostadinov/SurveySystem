using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Queries.GetSurveyById
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class GetSurveyByIdQueryHandler : IRequestHandler<GetSurveyByIdQuery, SurveySimpleViewModel>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly IMapper mapper;

        public GetSurveyByIdQueryHandler(IBmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SurveySimpleViewModel> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            return this.mapper.Map<SurveySimpleViewModel>(survey);
        }
    }
}
