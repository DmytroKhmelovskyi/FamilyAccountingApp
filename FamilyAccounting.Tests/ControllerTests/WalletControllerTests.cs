﻿using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FamilyAccounting.Tests.ControllerTests
{
    class WalletControllerTests
    {
        [Test]
        public void WalletController_CreateAnObject()
        {
            //Arrange
            string expected = "WalletController";
            var mockWallet = new Mock<IWalletWebService>();
            var mockPerson = new Mock<IPersonWebService>();

            //Act
            WalletController controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            //Arrange
            var mockWallet = new Mock<IWalletWebService>();
            var mockPerson = new Mock<IPersonWebService>();

            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            var result = controller.RedirectToAction("Delete");

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }

        [Test]
        public void Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var wallet = new WalletViewModel();
            var mockWallet = new Mock<IWalletWebService>();
            var mockPerson = new Mock<IPersonWebService>();
            mockWallet.Setup(g => g.Delete(wallet.Id));
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            ViewResult result = controller.Delete(wallet.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateShouldCallCreateWalletInBlOnce()
        {
            //Arrange
            WalletViewModel walletVM = new WalletViewModel
            {
                PersonId = 1,
                Description = "Description",
                Balance = 100
            };
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.Create(It.IsAny<WalletViewModel>())).Returns(It.IsAny<WalletViewModel>());

            //Act
            controller.Create(walletVM);

            //Assert
            mockWallet.Verify(x => x.Create(It.IsAny<WalletViewModel>()), Times.Once);
        }

        [Test]
        public void AddShouldRedirectToPersonDetails()
        {
            // Arrange
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Details");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Details"));
        }

        [Test]
        public void CreateShouldReturnViewResult()
        {
            // Arrange
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Create");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Create"));
        }


        [Test]
        public void Update_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var walletId = 1;
            var wallet = new WalletViewModel() { Id = walletId };
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            mockWallet.Setup(p => p.Update(walletId, It.IsAny<WalletViewModel>()));

            // Act
            var result = controller.Update(walletId, wallet);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
        }

        [Test]
        public void Update_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var wallet = new WalletViewModel()
            {
                Id = 1,
                Description = "for shopping",
            };
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            var result = controller.Update(wallet.Id, wallet);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MakeActiveShouldRedirectToWalletDetails()
        {
            // Arrange
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

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
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.MakeActive(id));

            //Act
            controller.MakeActive(id);

            //Assert
            mockWallet.Verify(x => x.MakeActive(id), Times.Once);
        }
    }
}

