//  ------------------------------------------------------------------------------------------------
//   <copyright file="SessionSurveyDto.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Models
{
    #region Using

    using System;
    using Application.Answers.Models;
    using Application.Surveys.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    #endregion

    public class SessionSurveyDto : SurveyDto
    {
        private const string SessionKey = "SurveyDto";

        [JsonIgnore] public ISession Session { get; set; }

        public static SurveyDto GetSurveyDto(IServiceProvider services)
        {
            var session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var survey = session?.GetJson<SessionSurveyDto>(SessionKey)
                         ?? new SessionSurveyDto();
            survey.Session = session;
            return survey;
        }

        public override void AddAnswer(AnswerViewModel answer)
        {
            base.AddAnswer(answer);
            Session.SetJson(SessionKey, this);
        }

        public override void SetAnswer(int questionId, string value)
        {
            base.SetAnswer(questionId, value);
            Session.SetJson(SessionKey, this);
        }

        public override void ClearAnswers()
        {
            base.ClearAnswers();
            Session.Remove(SessionKey);
        }
    }
}