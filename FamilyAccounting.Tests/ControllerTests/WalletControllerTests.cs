using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Models;
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
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            int id = 1;

            mockWallet.Setup(a => a.Get(id));
            WalletController controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            ViewResult result = controller.Details(id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Details_ThrowsException()
        {
            //Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            int id = 0;
            mockWallet.Setup(a => a.Get(id)).Throws(new Exception("Test Exception"));
            WalletController controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            ContentResult result = controller.Details(id) as ContentResult;

            //Assert
            Assert.That(() => mockWallet.Object.Get(id), Throws.Exception);
        }

        [Test]
        public void Details_VerifyOnce()
        {
            // arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            int id = 1;
            WalletController controller = new WalletController(mockWallet.Object, mockPerson.Object);

            // act
            RedirectToActionResult result = controller.Details(id) as RedirectToActionResult;

            // assert
            mockWallet.Verify(a => a.Get(id), Times.Once);
        }

        [Test]
        public void WalletController_CreateAnObject()
        {
            // arrange
            string expected = "WalletController";
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();

            // act
            WalletController controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }
        [Test]
        public void DeleteShouldCallDeleteWalletInBlOnce()
        {
            //Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();

            var controller = new WalletController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.Delete(1));

            //Act
            controller.DeleteWallet(1);

            //Assert
            mockWallet.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            // Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();

            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Delete");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }

        [Test]
        public void Delete_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var walletId = 1;
            var wallet = new WalletViewModel() { Id = walletId };
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();

            mockWallet.Setup(g => g.Delete(walletId));
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.DeleteWallet(walletId);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
        [Test]
        public void Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var wallet = new WalletViewModel();
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            mockWallet.Setup(g => g.Delete(wallet.Id));
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            ViewResult result = controller.Delete(wallet.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}

