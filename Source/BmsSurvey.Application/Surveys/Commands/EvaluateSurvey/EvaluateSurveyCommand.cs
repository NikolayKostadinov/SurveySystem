using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Surveys.Commands.EvaluateSurvey
{
    using MediatR;
    using Models;

    public class EvaluateSurveyCommand:IRequest<EvaluateSurveyViewModel>
    {
        public int Id { get; set; }
    }
}
