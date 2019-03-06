//-----------------------------------------------------------------------
// <copyright file="SurveyDtoTests.cs" company="Business Management System Ltd.">
//     Copyright "2019" (c) Business Management System Ltd. All rights reserved.
// </copyright>
// <author>Nikolay.Kostadinov</author>
// <convention>
// Test method convention
// [UnitOfWork]_[Scenario]_[ExpectedBehavior]
// </convention>
//-----------------------------------------------------------------------

namespace BmsSurvey.Application.Tests.Surveys.Models
{
    #region Using 

    using System;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;
    using Answers.Models;
    using Application.Surveys.Models;

    #endregion

    /// <summary>
    /// Summary description for SurveyDtoTests
    /// </summary>
    [TestFixture]
    public class SurveyDtoTests
    {
        [Test]
        public void SurveyDto_AddAnswer_ShouldAddAnswer()
        {
            //Arrange
            var surveyDto = new SurveyDto();
            var count = surveyDto.Answers.Count;

            //Act
            surveyDto.AddAnswer(new AnswerViewModel());

            //Assert
            Assert.AreEqual(count + 1, surveyDto.Answers.Count);
        }

        [Test]
        public void SurveyDto_AddAnswer_ShouldChangeExistAnswer()
        {
            //Arrange
            var surveyDto = new SurveyDto();
            var count = surveyDto.Answers.Count;
            surveyDto.AddAnswer(new AnswerViewModel() { QuestionId = 1, Value = "Test" });
            //Act
            surveyDto.AddAnswer(new AnswerViewModel() { QuestionId = 1, Value = "UpdatedTest" });


            //Assert
            Assert.AreEqual(1, surveyDto.Answers.Count);
            Assert.AreEqual("UpdatedTest", surveyDto.Answers[1].Value);
        }

        [Test]
        public void SurveyDto_SetAnswer_ShouldChangeExistAnswer()
        {
            //Arrange
            var surveyDto = new SurveyDto();
            surveyDto.AddAnswer(new AnswerViewModel() { QuestionId = 1, Value = "Test" });
            //Act
            surveyDto.SetAnswer(1, "UpdatedTest");


            //Assert
            Assert.AreEqual(1, surveyDto.Answers.Count);
            Assert.AreEqual("UpdatedTest", surveyDto.Answers[1].Value);
        }

        [Test]
        public void SurveyDto_SetAnswer_ShouldThrowKeyNotFounException()
        {
            //Arrange
            var surveyDto = new SurveyDto();
            surveyDto.AddAnswer(new AnswerViewModel() { QuestionId = 1, Value = "Test" });
            var count = surveyDto.Answers.Count;

            //Acr & Assert
            Assert.That(() => surveyDto.SetAnswer(2, "NewTest"),
                Throws.TypeOf<KeyNotFoundException>()
                    .With.Property("Message").Contains("key")
                    .With.Property("Message").Contains("2"));
        }

        [Test]
        public void SurveyDto_ClearAnswers_ShouldClearAllAnswer()
        {
            //Arrange
            var surveyDto = new SurveyDto();
            surveyDto.AddAnswer(new AnswerViewModel());
            surveyDto.AddAnswer(new AnswerViewModel());

            //Act
            surveyDto.ClearAnswers();

            //Assert
            Assert.AreEqual(0, surveyDto.Answers.Count);
        }
    }
}