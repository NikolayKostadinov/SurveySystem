namespace BmsSurvey.Application.Surveys.Commands.DeleteSurvey
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand>
    {
        private readonly BmsSurveyDbContext context;

        public DeleteSurveyCommandHandler(BmsSurveyDbContext context)
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
