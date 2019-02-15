//  ------------------------------------------------------------------------------------------------
//   <copyright file="IStatusExtentions.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Infrastructure.Extensions
{
    #region Using

    using System.Linq;
    using Common.Interfaces;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    #endregion

    public static class IStatusExtentions
    {
        public static void ToModelStateErrors(this IStatus status, ModelStateDictionary modelState)
        {
            foreach (var error in status.Errors)
                modelState.AddModelError(error.MemberNames.FirstOrDefault() ?? string.Empty, error.ErrorMessage);
        }
    }
}