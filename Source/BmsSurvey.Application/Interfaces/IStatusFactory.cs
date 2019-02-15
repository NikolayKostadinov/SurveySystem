//  ------------------------------------------------------------------------------------------------
//   <copyright file="IStatusFactory.cs" company="Business Management System Ltd.">
//       Copyright "2018" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------

namespace BmsSurvey.Application.Interfaces
{
    #region Using

    using System;
    using Common.Interfaces;

    #endregion

    public interface IStatusFactory
    {
        IStatus GetInstance(Type type);
    }
}