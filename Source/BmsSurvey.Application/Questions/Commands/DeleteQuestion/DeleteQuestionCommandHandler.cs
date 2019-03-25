//  ------------------------------------------------------------------------------------------------
//   <copyright file="DeleteQuestionCommandHandler.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Questions.Commands.DeleteQuestion
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using MediatR;

    public class DeleteQuestionCommandHandler:IRequestHandler<DeleteQuestionCommand,Unit>
    {
        private readonly IBmsSurveyDbContext context;

        public DeleteQuestionCommandHandler(IBmsSurveyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<Unit> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await this.context.Questions.FindAsync(request.Id);
            if (question != null)
            {
                this.context.Questions.Remove(question);
                await this.context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}