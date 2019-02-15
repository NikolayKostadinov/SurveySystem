using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BmsSurvey.WebApp.Infrastructure.Components
{
    using Application.Questions.Models;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(QuestionViewModel model) => View(model);
    }
}
