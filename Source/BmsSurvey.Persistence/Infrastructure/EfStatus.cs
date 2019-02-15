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
    using System.Linq;
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

    public class ExecutionStatus : IStatus
    {
        private List<ValidationResult> errors;

        public ExecutionStatus()
        {
            errors = new List<ValidationResult>();
        }

        /// <summary>
        ///     If there are no errors then it is valid
        /// </summary>
        public bool IsValid => !errors.Any();

        public IReadOnlyList<ValidationResult> Errors => errors ?? new List<ValidationResult>();

        public IStatus SetErrors(IEnumerable<ValidationResult> errorsParam)
        {
            errors = errorsParam.ToList();
            return this;
        }

        public void AppendError(ValidationResult error)
        {
            if (error != null) errors.Add(error);
        }

        public object Result { get; set; }
    }
}