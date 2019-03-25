using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.CreateSurvey
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces;
    using MediatR;

    public class CreateSurveyCommandHandler:IRequestHandler<CreateSurveyCommand,int>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly IMapper mapper;

        public CreateSurveyCommandHandler(IBmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = this.mapper.Map<Survey>(request);
            this.context.Surveys.Add(survey);
            await this.context.SaveChangesAsync(cancellationToken);
            return survey.Id;
        }
    }
}
