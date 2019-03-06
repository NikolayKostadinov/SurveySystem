//  ------------------------------------------------------------------------------------------------
//   <copyright file="OperationFailedException.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class OperationFailedException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public OperationFailedException(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}{string.Join(Environment.NewLine, Errors)}{Environment.NewLine}{this.StackTrace}";
        }
    }
}