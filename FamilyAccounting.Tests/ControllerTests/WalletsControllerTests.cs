using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class WalletsControllerTests
    {
        [Test]
        public void WalletController_CreateAnObject()
        {
            //Arrange
            string expected = "WalletsController";
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();

            //Act
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void MakeActiveShouldRedirectToWalletDetails()
        {
            // Arrange
            var mockPerson = new Mock<IPersonService>();
            var mockWallet = new Mock<IWalletService>();
            var controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Details");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Details"));
        }

        [Test]
        public void MakeActiveVerifyOnce()
        {
            //Arrange
            int id = 1;
            var mockPerson = new Mock<IPersonService>();
            var mockWallet = new Mock<IWalletService>();
            var controller = new WalletsController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.MakeActive(id));

            //Act
            controller.MakeActive(id);

            //Assert
            mockWallet.Verify(x => x.MakeActive(id), Times.Once);
        }
    }
}
