using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BmsSurvey.WebApp.Controllers
{
    using Application.Exceptions;
    using Application.Questions.Models;
    using Application.Surveys.Models;
    using Domain.Entities;
    using Microsoft.AspNetCore.Http;

    public class TestsController : BaseController
    {

        [IgnoreAntiforgeryToken]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }
 }