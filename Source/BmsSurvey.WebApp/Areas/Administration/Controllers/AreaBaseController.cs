//  ------------------------------------------------------------------------------------------------
//   <copyright file="AreaBaseController.cs" company="Business Management System Ltd.">
//       Copyright "2019" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Areas.Administration.Controllers
{
    #region Using

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebApp.Controllers;

    #endregion

    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public abstract class AreaBaseController : BaseController
    {
    }
}