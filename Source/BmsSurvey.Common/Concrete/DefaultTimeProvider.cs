//  ------------------------------------------------------------------------------------------------
//   <copyright file="DefaultTimeProvider.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Common.Concrete
{
    #region Using

    using System;
    using Abstract;

    #endregion

    /// <summary>
    ///     Summary description for DefaultTimeProvider
    /// </summary>
    public class DefaultTimeProvider : TimeProvider
    {
        public override DateTime UtcNow => DateTime.UtcNow;
    }
}