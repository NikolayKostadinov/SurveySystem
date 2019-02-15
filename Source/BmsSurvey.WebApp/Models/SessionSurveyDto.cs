using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Models
{
    using System.Reflection.Metadata;
    using Application.Questions.Models;
    using Application.Surveys.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public class SessionSurveyDto : SurveyDto
    {
        private const string SessionKey = "SurveyDto";
        public static SurveyDto GetSurveyDto(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionSurveyDto survey = session?.GetJson<SessionSurveyDto>(SessionKey)
                               ?? new SessionSurveyDto();
            survey.Session = session;
            return survey;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddQuestion(QuestionSimpleViewModel question)
        {
            base.AddQuestion(question);
            Session.SetJson(SessionKey, this);
        }

        public override void SetAnswer(int questionId, string value)
        {
            base.SetAnswer(questionId, value);
            Session.SetJson(SessionKey, this);
        }
    }
}
