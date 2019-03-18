namespace BmsSurvey.Application.Questions.Commands.EvaluateQuestion.Handlers
{
    using Microsoft.Extensions.DependencyInjection;

    public static class QuestionEvaluatorConfig
    {
        public static IServiceCollection AddQuestionEvaluator(this IServiceCollection services)
        {
            services.AddScoped<Rate1To5StarsQuestionEvaluationHandler>();
            services.AddScoped<YesOrNoAnswerQuestionEvaluationHandler>();
            services.AddScoped<LowMidHighAnswerQuestionEvaluationHandler>();
            services.AddScoped<FreeTextAnswerQuestionEvaluationHandler>();

            services.AddScoped<IQuestionEvaluationHandler>(ctx =>
            {
                var rate1To5Handler = ctx.GetService<Rate1To5StarsQuestionEvaluationHandler>();
                var yesOrNoHandler = ctx.GetService<YesOrNoAnswerQuestionEvaluationHandler>();
                var lowMidHighHandler = ctx.GetService<LowMidHighAnswerQuestionEvaluationHandler>();
                var freeTextHandler = ctx.GetService<FreeTextAnswerQuestionEvaluationHandler>();

                rate1To5Handler.SetSuccessor(yesOrNoHandler);
                yesOrNoHandler.SetSuccessor(lowMidHighHandler);
                lowMidHighHandler.SetSuccessor(freeTextHandler);

                return rate1To5Handler;
            });

            services.AddScoped<IQuestionEvaluationHandlerFactory, QuestionEvaluationHandlerFactory>();
            services.AddSingleton<IPercentCorrectionService, PercentCorrectionService>();

            return services;
        }
    }
}
