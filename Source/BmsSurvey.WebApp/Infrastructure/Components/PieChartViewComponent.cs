using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Components
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class PieChartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(Dictionary<string,decimal> pieData, QuestionType pieType) => View(new PieChartViewModel(pieData, pieType));

    }
}
