using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.ControllerTests
{
    public class CardsControllerTests
    {
        [Test]
        public void CardsController_CreateAnObject()
        {
            //Arrange
            string expected = "CardsController";
            var mockCard = new Mock<ICardService>();

            //Act
            CardsController controller = new CardsController(mockCard.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public async Task CreateCard_ShouldBeNotNull()
        {
            var card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            // Arrange
            var mockCard = new Mock<ICardService>();
            mockCard.Setup(a => a.CreateAsync(card)).ReturnsAsync(card);
            CardsController controller = new CardsController(mockCard.Object);

            // Act
            var result = await controller.Create(card) as OkObjectResult;

            // Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);

        }

        [Test]
        public async Task CreateCard_ShouldCallCreateCardInBlOnce()
        {
            //Arrange
            CardDTO cardVM = new CardDTO
            {
                Description = "Description",
            };
            var mockCard = new Mock<ICardService>();
            CardsController controller = new CardsController(mockCard.Object);
            mockCard.Setup(x => x.CreateAsync(It.IsAny<CardDTO>())).ReturnsAsync(It.IsAny<CardDTO>());

            //Act
            await controller.Create(cardVM);

            //Assert
            mockCard.Verify(x => x.Create(It.IsAny<CardDTO>()), Times.Once);
        }

        [Test]
        public async Task Details_ViewResultNotNull()
        {
            //Arrange
            var testCard = new CardDTO
            {
                WalletId = 3,
                Number = "6011111111111117",
                Description = "O.O.N.Q.D.U.Y.U.R.D"
            };
            var mockСard = new Mock<ICardService>();
            mockСard.Setup(a => a.GetAsync(testCard.WalletId)).ReturnsAsync(testCard);
            var controller = new CardsController(mockСard.Object);

            //Act
            var result = await controller.Details(testCard.WalletId) as OkObjectResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, testCard);
        }

        [Test]
        public async Task Details_VerifyOnce()
        {
            //Arrange
            var WalletId = 1;
            var mockCard = new Mock<ICardService>();
            var controller = new CardsController(mockCard.Object);

            //Act
            var result = await controller.Details(1) as OkObjectResult;

            //Assert
            mockCard.Verify(a => a.Get(WalletId), Times.Once);
        }

        [Test]
        public async Task UpdateCard_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var mockСard = new Mock<ICardService>();
            var controller = new CardsController(mockСard.Object);

            //Act
            var result = await controller.Update(card.WalletId, card) as OkObjectResult;

            //Assert
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Update_VerifyOnce()
        {
            //Arrange
            CardDTO card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var mockCard = new Mock<ICardService>();
            var controller = new CardsController(mockCard.Object);
            mockCard.Setup(a => a.Update(card.WalletId, card));

            //Act
            var result = await controller.Update(card.WalletId, card) as OkResult;

            //Assert
            mockCard.Verify(a => a.Update(card.WalletId, card), Times.Once);
        }
        [Test]
        public void DeleteCard_ShouldRedirectToActionDelete()
        {
            //Arrange
            var mockCard = new Mock<ICardService>();

            var controller = new CardsController(mockCard.Object);

            //Act
            var result = controller.RedirectToAction("Delete");

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }
        [Test]
        public async Task DeleteCard_ShouldReturnOkResult()
        {
            //Arrange
            var walletId = 1;
            var mockCard = new Mock<ICardService>();
            mockCard.Setup(g => g.Delete(walletId));
            var controller = new CardsController(mockCard.Object);

            //Act
            var result = await controller.DeleteCard(walletId) as OkResult;

            //Assert
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task Delete_ResultIsNotNull()
        {
            //Arrange
            var walletId = 1;
            var mockCard = new Mock<ICardService>();
            mockCard.Setup(g => g.Delete(walletId));
            var controller = new CardsController(mockCard.Object);

            //Act
            var result = await controller.DeleteCard(walletId) as OkResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
