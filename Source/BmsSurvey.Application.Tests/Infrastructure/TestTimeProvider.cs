//  ------------------------------------------------------------------------------------------------
//   <copyright file="TestTimeProvider.cs" company="Business Management System Ltd.">
//       Copyright "" (c), Business Management System Ltd. 
//       All rights reserved.
//   </copyright>
//   <author>Nikolay.Kostadinov</author>
//  ------------------------------------------------------------------------------------------------
namespace BmsSurvey.Application.Tests.Infrastructure
{
    using System;
    using Common.Abstract;

    public class TestTimeProvider : TimeProvider
    {
        public override DateTime UtcNow => new DateTime(2019, 2, 25, 5, 30, 12);
    }
}