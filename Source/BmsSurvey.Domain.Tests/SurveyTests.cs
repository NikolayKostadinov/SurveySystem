//-----------------------------------------------------------------------
// <copyright file="SurveyTests.cs" company="Business Management System Ltd.">
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

    using Common.Abstract;
    using Entities;
    using NUnit.Framework;

    #endregion

    /// <summary>
    /// Summary description for NKUnitTest1
    /// </summary>
    [TestFixture]
    public class SurveyTests
    {
        [Test]
        public void Survey_IfActiveFromIsBeforeTodayAndActiveToIsAfterToday_IsActive()
        {
            //Arrange
            var activeFrom = TimeProvider.Current.UtcNow.Date.AddDays(-1);
            var activeTo = TimeProvider.Current.UtcNow.Date.AddDays(1);

            //Act
            var survey = new Survey() { ActiveFrom = activeFrom, ActiveTo = activeTo };

            //Assert
            Assert.IsTrue(survey.IsActive);
        }

        [Test]
        public void Survey_IfActiveFromIsTodayAndActiveToIsAfterToday_IsActive()
        {
            //Arrange
            var activeFrom = TimeProvider.Current.UtcNow.Date;
            var activeTo = TimeProvider.Current.UtcNow.Date.AddDays(1);

            //Act
            var survey = new Survey() { ActiveFrom = activeFrom, ActiveTo = activeTo };

            //Assert
            Assert.IsTrue(survey.IsActive);
        }

        [Test]
        public void Survey_IfActiveFromIsBeforeTodayAndActiveToIsToday_IsActive()
        {
            //Arrange
            var activeFrom = TimeProvider.Current.UtcNow.Date.AddDays(-1);
            var activeTo = TimeProvider.Current.UtcNow.Date;

            //Act
            var survey = new Survey() { ActiveFrom = activeFrom, ActiveTo = activeTo };

            //Assert
            Assert.IsTrue(survey.IsActive);
        }

        [Test]
        public void Survey_IfActiveFromIsBeforeTodayAndActiveToIsBefore_IsNotActive()
        {
            //Arrange
            var activeFrom = TimeProvider.Current.UtcNow.Date.AddDays(-2);
            var activeTo = TimeProvider.Current.UtcNow.Date.AddDays(-1);

            //Act
            var survey = new Survey() { ActiveFrom = activeFrom, ActiveTo = activeTo };

            //Assert
            Assert.IsFalse(survey.IsActive);
        }

        [Test]
        public void Survey_IfActiveFromIsAfterTodayAndActiveToIsAfter_IsNotActive()
        {
            //Arrange
            var activeFrom = TimeProvider.Current.UtcNow.Date.AddDays(1);
            var activeTo = TimeProvider.Current.UtcNow.Date.AddDays(2);

            //Act
            var survey = new Survey() { ActiveFrom = activeFrom, ActiveTo = activeTo };

            //Assert
            Assert.IsFalse(survey.IsActive);
        }
    }
}