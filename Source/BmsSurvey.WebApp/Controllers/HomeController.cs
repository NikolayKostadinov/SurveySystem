﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BmsSurvey.WebApp.Models;

namespace BmsSurvey.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AcceptCookies(string returnUrl)
        {
            return View((object)returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
