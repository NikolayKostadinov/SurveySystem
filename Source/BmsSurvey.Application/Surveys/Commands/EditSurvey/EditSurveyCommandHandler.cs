namespace BmsSurvey.Application.Surveys.Commands.EditSurvey
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class EditSurveyCommandHandler : IRequestHandler<EditSurveyCommand, int>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly IMapper mapper;

        public EditSurveyCommandHandler(IBmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> Handle(EditSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys.FirstOrDefaultAsync(s => s.Id == request.Id && s.IsDeleted == false);
            if (survey is null)
            {
                throw new NotFoundException(nameof(survey), request.Id);
            }

            this.mapper.Map(request, survey);
            var result = await this.context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
