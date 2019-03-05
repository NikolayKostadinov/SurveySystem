//  ------------------------------------------------------------------------------------------------
//   <copyright file="EfStatus.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Persistence.Infrastructure
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Concrete;
    using Common.Interfaces;

    #endregion

    public class EfStatus : ExecutionStatus, IEfStatus
    {
        public int ResultRecordsCount { get; set; }

        ///// <summary>
        ///// This converts the Entity framework errors into Validation Errors
        ///// </summary>
        //public IEfStatus SetEfErrors(IEnumerable<ValidationResult> dbErrors)
        //{
        //    base.SetErrors(
        //        dbErrors.SelectMany(
        //            x => x.ValidationErrors.Select(y =>
        //                  new ValidationResult(y.ErrorMessage, new[] { y.PropertyName })))
        //            .ToList());
        //    return this;
        //}

        public IEfStatus SetEfErrors(IEnumerable<ValidationResult> errors)
        {
            SetErrors(errors);
            return this;
        }
    }
}