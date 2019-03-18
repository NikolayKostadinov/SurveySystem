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
    using EditingWithServerValidation.Models;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNetCore.Http;

    public class TestsController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [IgnoreAntiforgeryToken]
        public ActionResult _Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetProducts().ToDataSourceResult(request));
        }

        [IgnoreAntiforgeryToken]
        public ActionResult _Create([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //Manually add an model error in order to simulate validation error
            ModelState.AddModelError("Name", "My server error");



            //TODO: implement create functionality

            return Json(ModelState.ToDataSourceResult());
        }

        [IgnoreAntiforgeryToken]
        public ActionResult _Update([DataSourceRequest] DataSourceRequest request, Product product)
        {
            //Manually add an model error in order to simulate validation error
            ModelState.AddModelError("", "My server error");


            //TODO: implement update functionality

            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<Product> GetProducts()
        {
            return Enumerable.Range(1, 10).Select(i => new Product
            {
                Id = i,
                Name = "Product" + i
            });
        }


    }
 }