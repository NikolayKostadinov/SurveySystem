//  ------------------------------------------------------------------------------------------------
//   <copyright file="AreaBaseController.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.WebApp.Areas.SurveySupport.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Controllers;

    [Area("SurveySupport")]
    [Authorize(Roles = "Administrator, SurveySupporter")]
    public class AreaBaseController : BaseController
    {
    }
}