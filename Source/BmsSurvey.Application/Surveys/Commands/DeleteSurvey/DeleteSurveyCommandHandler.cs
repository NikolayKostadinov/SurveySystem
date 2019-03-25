namespace BmsSurvey.Application.Surveys.Commands.DeleteSurvey
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand>
    {
        private readonly IBmsSurveyDbContext context;

        public DeleteSurveyCommandHandler(IBmsSurveyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Unit> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys.FindAsync(request.Id);
            if (survey != null)
            {
                this.context.Surveys.Remove(survey);
                await this.context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
