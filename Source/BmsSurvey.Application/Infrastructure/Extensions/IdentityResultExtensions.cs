using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Application.Infrastructure.Extensions
{
    using System.Linq;
    using Exceptions;
    using Microsoft.AspNetCore.Identity;
    using Users.Commands.EditUser;

    public static class IdentityResultExtensions
    {
        /// <summary>Checks identity result for errors. If there is error throws "OperationFailedException".</summary>
        /// <param name="result">The result.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <exception cref="OperationFailedException">If there are errors in identity result</exception>
        public static void Check(this IdentityResult result, string operationName)
        {
            if (!result.Succeeded)
            {
                var errors = new[] { $"Operation: {operationName}" }
                    .Union(result.Errors.Select(x => x.Description).ToArray());
                throw new OperationFailedException(errors);
            }
        }
    }
}
