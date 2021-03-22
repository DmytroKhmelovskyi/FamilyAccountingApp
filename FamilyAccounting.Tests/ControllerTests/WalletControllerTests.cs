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
        [Test]
        public void DeleteShouldCallDeleteWalletInBlOnce()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            var controller = new WalletController(mock.Object);
            mock.Setup(x => x.Delete(1));

            //Act
            controller.DeleteWallet(1);

            //Assert
            mock.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            // Arrange
            var mock = new Mock<IWalletService>();
            var controller = new WalletController(mock.Object);

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
            var walletGuest = new WalletViewModel() { Id = walletId };
            var walletRepo = new Mock<IWalletService>();
            walletRepo.Setup(g => g.Delete(walletId));
            var controller = new WalletController(walletRepo.Object);

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
            var mock = new Mock<IWalletService>();
            mock.Setup(g => g.Delete(wallet.Id));
            var controller = new WalletController(mock.Object);
            //Act
            ViewResult result = controller.Delete(wallet.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}

