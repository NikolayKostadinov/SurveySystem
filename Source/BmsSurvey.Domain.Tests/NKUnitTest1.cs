//-----------------------------------------------------------------------
// <copyright file="NKUnitTest1.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Domain.Tests
{
    #region Using 

    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Summary description for NKUnitTest1
    /// </summary>
    [TestFixture]
    public class NKUnitTest1
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(1,1);
        }
    }
}