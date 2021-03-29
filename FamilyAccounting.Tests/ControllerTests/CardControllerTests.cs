﻿using FamilyAccounting.Web.Controllers;
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
    }
}
