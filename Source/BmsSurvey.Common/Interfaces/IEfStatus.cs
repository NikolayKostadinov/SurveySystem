//  ------------------------------------------------------------------------------------------------
//   <copyright file="IEfStatus.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Common.Interfaces
{
    #region Using

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    ///     Status of current operation
    /// </summary>
    public interface IEfStatus : IStatus
    {
        int ResultRecordsCount { get; set; }

        IEfStatus SetEfErrors(IEnumerable<ValidationResult> errors);
    }
}