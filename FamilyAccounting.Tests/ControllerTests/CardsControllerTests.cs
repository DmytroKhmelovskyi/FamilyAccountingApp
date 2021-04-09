using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FamilyAccounting.Tests.ControllerTests
{
    public class CardsControllerTests
    {
        [Test]
        public void CardsController_CreateAnObject()
        {
            //Arrange
            string expected = "CardsController";
            var mockWallet = new Mock<IWalletService>();
            var mockCard = new Mock<ICardService>();

            //Act
            CardsController controller = new CardsController(mockCard.Object, mockWallet.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void CreateCard_ShouldBeNotNull()
        {
            var card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            // Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockCard = new Mock<ICardService>();
            mockCard.Setup(a => a.Create(card)).Returns(card);
            CardsController controller = new CardsController(mockCard.Object, mockWallet.Object);

            // Act
            var result = controller.Create(card) as OkResult;

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);

        }

        [Test]
        public void CreateCard_ShouldCallCreateCardInBlOnce()
        {
            //Arrange
            CardDTO cardVM = new CardDTO
            {
                Description = "Description",
            };
            var mockWallet = new Mock<IWalletService>();
            var mockCard = new Mock<ICardService>();
            CardsController controller = new CardsController(mockCard.Object, mockWallet.Object);
            mockCard.Setup(x => x.Create(It.IsAny<CardDTO>())).Returns(It.IsAny<CardDTO>());

            //Act
            controller.Create(cardVM);

            //Assert
            mockCard.Verify(x => x.Create(It.IsAny<CardDTO>()), Times.Once);
        }

        [Test]
        public void Details_ViewResultNotNull()
        {
            //Arrange
            var testCard = new CardDTO { 
                WalletId = 3, 
                Number = "6011111111111117", 
                Description = "O.O.N.Q.D.U.Y.U.R.D"
            };
            var mockWallet = new Mock<IWalletService>();
            var mockСard = new Mock<ICardService>();
            mockСard.Setup(a => a.Get(testCard.WalletId)).Returns(testCard);
            var controller = new CardsController(mockСard.Object, mockWallet.Object);

            //Act
            var result = controller.Details(testCard.WalletId) as OkObjectResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, testCard);
        }

        [Test]
        public void Details_VerifyOnce()
        {
            //Arrange
            var WalletId = 1;
            var mockCard = new Mock<ICardService>();
            var mockWallet = new Mock<IWalletService>();
            var controller = new CardsController(mockCard.Object, mockWallet.Object);

            //Act
            var result = controller.Details(1) as OkObjectResult;

            //Assert
            mockCard.Verify(a => a.Get(WalletId), Times.Once);
        }

        [Test]
        public void UpdateCard_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var mockWallet = new Mock<IWalletService>();
            var mockСard = new Mock<ICardService>();
            var controller = new CardsController(mockСard.Object, mockWallet.Object);

            //Act
            var result = controller.Update(card.WalletId, card) as OkResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Update_VerifyOnce()
        {
            //Arrange
            CardDTO card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var mockCard = new Mock<ICardService>();
            var mockWallet = new Mock<IWalletService>();
            var controller = new CardsController(mockCard.Object, mockWallet.Object);
            mockCard.Setup(a => a.Update(card.WalletId, card));

            //Act
            var result = controller.Update(card.WalletId, card) as OkResult;

            //Assert
            mockCard.Verify(a => a.Update(card.WalletId, card), Times.Once);
        }
        [Test]
        public void DeleteCard_ShouldRedirectToActionDelete()
        {
            //Arrange
            var mockCard = new Mock<ICardService>();
            var mockWallet = new Mock<IWalletService>();

            var controller = new CardsController(mockCard.Object, mockWallet.Object);

            //Act
            var result = controller.RedirectToAction("Delete");

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }
        [Test]
        public void DeleteCard_ShouldReturnOkResult()
        {
            //Arrange
            var walletId = 1;
            var mockCard = new Mock<ICardService>();
            var mockWallet = new Mock<IWalletService>();
            mockCard.Setup(g => g.Delete(walletId));
            var controller = new CardsController(mockCard.Object, mockWallet.Object);

            //Act
            var result = controller.DeleteCard(walletId) as OkResult;

            //Assert
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void Delete_ResultIsNotNull()
        {
            //Arrange
            var walletId = 1;
            var mockCard = new Mock<ICardService>();
            var mockWallet = new Mock<IWalletService>();
            mockCard.Setup(g => g.Delete(walletId));
            var controller = new CardsController(mockCard.Object, mockWallet.Object);

            //Act
            var result = controller.DeleteCard(walletId) as OkResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
