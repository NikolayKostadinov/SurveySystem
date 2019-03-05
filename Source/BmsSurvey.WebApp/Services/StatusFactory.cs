//  ------------------------------------------------------------------------------------------------
//   <copyright file="StatusFactory.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.WebApp.Services
{
    #region Using

    using System;
    using Application.Interfaces;
    using Common.Concrete;
    using Common.Interfaces;
    using Persistence.Infrastructure;

    #endregion

    public class StatusFactory : IStatusFactory
    {
        public IStatus GetInstance(Type type)
        {
            switch (type.Name)
            {
                case "IEfStatus":
                    return new EfStatus();
                case "IStatus":
                    return new ExecutionStatus();
                default:
                    throw new ArgumentOutOfRangeException($"Unknown type: {type.FullName}");
            }
        }
    }
}