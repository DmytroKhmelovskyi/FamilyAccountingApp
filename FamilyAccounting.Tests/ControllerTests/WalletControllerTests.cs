using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace FamilyAccounting.Tests.ControllerTests
{
    class WalletControllerTests
    {
        [Test]
        public void Details_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            int id = 1;

            mock.Setup(a => a.Get(id));
            WalletController controller = new WalletController(mock.Object);

            //Act
            ViewResult result = controller.Details(id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Details_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            int id = 0;
            mock.Setup(a => a.Get(id)).Throws(new Exception("Test Exception"));
            WalletController controller = new WalletController(mock.Object);

            //Act
            ContentResult result = controller.Details(id) as ContentResult;

            //Assert
            Assert.That(() => mock.Object.Get(id), Throws.Exception);
        }

        [Test]
        public void Details_VerifyOnce()
        {
            // arrange
            var mock = new Mock<IWalletService>();
            int id = 1;
            WalletController controller = new WalletController(mock.Object);

            // act
            RedirectToActionResult result = controller.Details(id) as RedirectToActionResult;

            // assert
            mock.Verify(a => a.Get(id), Times.Once);
        }

        [Test]
        public void WalletController_CreateAnObject()
        {
            // arrange
            string expected = "WalletController";
            var mock = new Mock<IWalletService>();

            // act
            WalletController controller = new WalletController(mock.Object);

            //assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }
    }
}
