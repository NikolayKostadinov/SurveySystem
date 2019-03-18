using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Components
{
    using Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class AnswerTableViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Dictionary<string,int> answerData) => View(answerData);

    }
}
