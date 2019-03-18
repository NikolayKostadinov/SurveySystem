using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Components.Models
{
    using Domain.Entities;

    public class PieChartViewModel
    {
        public PieChartViewModel(Dictionary<string, decimal> data, QuestionType questionType)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            QuestionType = questionType;
        }

        public Dictionary<string, decimal> Data { get; }
        public QuestionType QuestionType { get; }

    }
}
