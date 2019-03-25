using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Controllers
{
    using Application.Exceptions;
    using Application.Questions.Models;
    using Application.Surveys.Commands.EvaluateSurvey;
    using Application.Surveys.Models;
    using Domain.Entities;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Http;

    public class TestsController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

    }
 }