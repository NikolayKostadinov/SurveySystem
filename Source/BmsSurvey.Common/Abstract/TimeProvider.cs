//-----------------------------------------------------------------------
// <copyright file="TimeProvider.cs" company="Business Management System Ltd.">
//     Copyright "2017" (c) Business Management System Ltd.. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
//-----------------------------------------------------------------------

namespace BmsSurvey.Common.Abstract
{
    #region Usings

    using System;
    using Concrete;

    #endregion

    /// <summary>
    /// Summary description for TimeProvider
    /// </summary>
    public abstract class TimeProvider
    {
        private static TimeProvider current;

        static TimeProvider()
        {
            current = new DefaultTimeProvider();
        }

        public static TimeProvider Current
        {
            get => current;
            set => current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract DateTime UtcNow { get; }

        public static void ResetToDefault()
        {
            TimeProvider.current = new DefaultTimeProvider();
        }
    }

    
}