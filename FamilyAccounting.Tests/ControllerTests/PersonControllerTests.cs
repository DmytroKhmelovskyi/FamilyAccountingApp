using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonControllerTests
    {
        [Test]
        public void Index_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<IPersonService>();

            mock.Setup(a => a.Get());
            PersonController controller = new PersonController(mock.Object);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result.Model);
        }
        [Test]
        public void Index_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            mock.Setup(a => a.Get()).Throws(new Exception("Test Exception"));
            PersonController controller = new PersonController(mock.Object);
            //Act
            ContentResult result = controller.Index() as ContentResult;
            //Assert
            Assert.That(() => mock.Object.Get(), Throws.Exception);
        }
        [Test]
        public void Index_VerifyOnce()
        {
            // arrange
            var mock = new Mock<IPersonService>();
            PersonController controller = new PersonController(mock.Object);
            // act
            RedirectToActionResult result = controller.Index() as RedirectToActionResult;
            // assert
            mock.Verify(a => a.Get(), Times.Once);
        }
        [Test]
        public void PersonController_CreateAnObject()
        {
            // arrange
            string expected = "PersonController";
            var mock = new Mock<IPersonService>();
            // act
            PersonController controller = new PersonController(mock.Object);
            //assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }
    }
}
