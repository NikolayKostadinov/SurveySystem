//-----------------------------------------------------------------------
// <copyright file="AnswerFactory.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Services
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Answers.Models;
    using Application.Services;
    using Domain.Entities;
    using Domain.Entities.Answers;

    #endregion

    /// <summary>
    /// Summary description for AnswerFactory
    /// </summary>
    [TestFixture]
    public class AnswerFactoryTests
    {
        [Test]
        public void GetAnswer_Rate1to5Stars_ValidAnswer()
        {
            //Arrange
            var factory = new AnswerFactory();
            //Act
            var result = factory.GetAnswer(QuestionType.Rate1to5Stars,
                new AnswerViewModel() { QuestionId = 1, Value = "5" });
            //Assert
            Assert.IsInstanceOf<Rate1To5StarsAnswer>(result);
            Assert.AreEqual(1, result.QuestionId);
            Assert.AreEqual(5, ((Rate1To5StarsAnswer)result).Value);
        }

        [Test]
        public void GetAnswer_LowMidHigh_ValidAnswer()
        {
            //Arrange
            var factory = new AnswerFactory();
            //Act
            var result = factory.GetAnswer(QuestionType.LowMidHigh,
                new AnswerViewModel() { QuestionId = 1, Value = "2" });
            //Assert
            Assert.IsInstanceOf<LowMidHighAnswer>(result);
            Assert.AreEqual(1, result.QuestionId);
            Assert.AreEqual(2, ((LowMidHighAnswer)result).Value);
        }

        [Test]
        public void GetAnswer_YesOrNo_ValidAnswer()
        {
            //Arrange
            var factory = new AnswerFactory();
            //Act
            var result = factory.GetAnswer(QuestionType.YesOrNo,
                new AnswerViewModel() { QuestionId = 1, Value = "True" });
            //Assert
            Assert.IsInstanceOf<YesOrNoAnswer>(result);
            Assert.AreEqual(1, result.QuestionId);
            Assert.IsTrue(((YesOrNoAnswer)result).Value);
        }

        [Test]
        public void GetAnswer_FreeText_ValidAnswer()
        {
            //Arrange
            var factory = new AnswerFactory();
            //Act
            var result = factory.GetAnswer(QuestionType.FreeText,
                new AnswerViewModel() { QuestionId = 1, Value = "TestValue" });
            //Assert
            Assert.IsInstanceOf<FreeTextAnswer>(result);
            Assert.AreEqual(1, result.QuestionId);
            Assert.AreEqual("TestValue", ((FreeTextAnswer)result).Value);
        }

        [Test]
        public void GetAnswer_Unknown_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            var factory = new AnswerFactory();
            var unknownType = (QuestionType) 6;
            
            //Act & Assert
            Assert.That(() => factory.GetAnswer(unknownType, new AnswerViewModel() { QuestionId = 1, Value = "TestValue" }),
                Throws.TypeOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName").Contains("questionType")
                    .With.Property("Message").Contains("6"));
        }
    }
}