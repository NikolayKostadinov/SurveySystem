//  ------------------------------------------------------------------------------------------------
//   <copyright file="IStatus.cs" company="Business Management System Ltd.">
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
    ///     Summary description for IStatus
    /// </summary>
    public interface IStatus
    {
        IReadOnlyList<ValidationResult> Errors { get; }
        bool IsValid { get; }
        IStatus SetErrors(IEnumerable<ValidationResult> errorsParam);

        void AppendError(ValidationResult error);

        object Result { get; set; }
    }
}