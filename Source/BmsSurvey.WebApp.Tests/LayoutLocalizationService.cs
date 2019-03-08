namespace BmsSurvey.WebApp.Tests
{
    using Controllers;
    using Microsoft.AspNetCore.Mvc;
    using NUnit.Framework;

    public class LayoutLocalizationService
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void  TestController_Index_MustReturnView()
        {
            //Arrange
            var controller = new TestsController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}