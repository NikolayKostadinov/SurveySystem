//  ------------------------------------------------------------------------------------------------
//   <copyright file="ExecutionStatus.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Common.Concrete
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Interfaces;

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