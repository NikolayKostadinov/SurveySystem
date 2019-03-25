namespace BmsSurvey.Application.Questions.Commands.CreateQuestion
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces;
    using MediatR;
    using Models;

    public class CreateQuestionCommandHandler:IRequestHandler<CreateQuestionCommand,QuestionListViewModel>
    {
        private readonly IBmsSurveyDbContext context;
        private readonly IMapper mapper;

        public CreateQuestionCommandHandler(IBmsSurveyDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<QuestionListViewModel> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = this.mapper.Map<Question>(request);
            this.context.Questions.Add(question);
            await this.context.SaveChangesAsync(cancellationToken);
            return this.mapper.Map<QuestionListViewModel>(question);
        }
    }
}
