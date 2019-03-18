using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.EvaluateSurvey
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Persistence;
    using Questions.Commands.EvaluateQuestion.Handlers;
    using Questions.Models.EvaluationModels;

    public class EvaluateSurveyCommandHandler:IRequestHandler<EvaluateSurveyCommand, EvaluateSurveyViewModel>
    {
        private readonly IMapper mapper;
        private readonly BmsSurveyDbContext context;
        private readonly IQuestionEvaluationHandlerFactory factory;

        public EvaluateSurveyCommandHandler(IMapper mapper, 
            BmsSurveyDbContext context,
            IQuestionEvaluationHandlerFactory factory)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<EvaluateSurveyViewModel> Handle(EvaluateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await this.context.Surveys
                .Include(s=>s.CompletedSurveys)
                .Include(s=>s.Questions)
                .ThenInclude(q=>q.Answers)
                .FirstOrDefaultAsync(s=>s.Id == request.Id && s.IsDeleted == false);

            if (survey is null)
            {
                throw new NotFoundException(nameof(survey), request.Id);
            }

            var evaluator = factory.GetHandler();

            var evaluatedQuestions = new List<BaseQuestionEvaluationModel>();
            foreach (var surveyQuestion in survey.Questions)
            {
                evaluatedQuestions.Add(await evaluator.EvaluateQuestionAsync(surveyQuestion));
            }

            var result = this.mapper.Map<EvaluateSurveyViewModel>(survey);
            result.EvaluatedQuestions = evaluatedQuestions.OrderBy(eq=>eq.DisplayNumber);

            return result;
        }
    }
}
