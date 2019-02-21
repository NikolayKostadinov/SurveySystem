namespace BmsSurvey.Application.Questions.Commands.EditQuestion
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;

    public class EditQuestionCommandHandler : IRequestHandler<EditQuestionCommand, QuestionListViewModel>
    {
        private readonly BmsSurveyDbContext context;
        private readonly IMapper mapper;

        public EditQuestionCommandHandler(BmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<QuestionListViewModel> Handle(EditQuestionCommand request, CancellationToken cancellationToken)
        {
            //todo:fix this
            var question = await this.context.Questions.FirstOrDefaultAsync(q => q.Id == request.Id && q.IsDeleted == false);
            if (question is null)
            {
                throw new NotFoundException(nameof(question), request.Id);
            }

            this.mapper.Map(request, question);
            var result = await this.context.SaveChangesAsync(cancellationToken);
            return this.mapper.Map<QuestionListViewModel>(question);
        }
    }
}
