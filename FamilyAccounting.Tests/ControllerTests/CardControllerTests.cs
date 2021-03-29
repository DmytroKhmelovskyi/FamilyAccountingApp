using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    public class CardControllerTests
    {
        [Test]
        public void CardController_CreateAnObject()
        {
            //Arrange
            string expected = "CardController";
            var mockWallet = new Mock<IWalletWebService>();
            var mockCard = new Mock<ICardWebService>();

            //Act
            CardController controller = new CardController(mockCard.Object, mockWallet.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void CreateCard_ShouldCallCreateCardInBlOnce()
        {
            //Arrange
            CardViewModel cardVM = new CardViewModel
            {
                Description = "Description",
            };
            var mockWallet = new Mock<IWalletWebService>();
            var mockCard = new Mock<ICardWebService>();
            CardController controller = new CardController(mockCard.Object, mockWallet.Object);
            mockCard.Setup(x => x.Create(It.IsAny<CardViewModel>())).Returns(It.IsAny<CardViewModel>());

            //Act
            controller.Create(cardVM);

            //Assert
            mockCard.Verify(x => x.Create(It.IsAny<CardViewModel>()), Times.Once);
        }

        [Test]
        public void CreateCard_ShouldReturnViewResult()
        {
            // Arrange
            var mockWallet = new Mock<IWalletWebService>();
            var mockCard = new Mock<ICardWebService>();
            CardController controller = new CardController(mockCard.Object, mockWallet.Object);

            // Act
            var result = controller.Create(1);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
        [Test]
        public void CreateCard_ShouldRedirectToWalletDetails()
        {
            // Arrange
            var mockWallet = new Mock<IWalletWebService>();
            var mockCard = new Mock<ICardWebService>();
            CardController controller = new CardController(mockCard.Object, mockWallet.Object);

            // Act
            var result = controller.RedirectToAction("Details");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Details"));
        }
        [Test]
        public void UpdateСard_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var walletId = 1;
            var card = new CardViewModel() { WalletId = walletId };
            var mockWallet = new Mock<IWalletWebService>();
            var mockСard = new Mock<ICardWebService>();
            var controller = new CardController(mockСard.Object, mockWallet.Object);
            mockСard.Setup(p => p.Update(walletId, It.IsAny<CardViewModel>()));

            // Act
            var result = controller.Update(walletId, card);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
        }

        [Test]
        public void UpdateCard_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var card = new CardViewModel()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var mockWallet = new Mock<IWalletWebService>();
            var mockСard = new Mock<ICardWebService>();

            var controller = new CardController(mockСard.Object, mockWallet.Object);

            //Act
            var result = controller.Update(card.WalletId, card);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
