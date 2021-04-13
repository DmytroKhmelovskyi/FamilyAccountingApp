using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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
        public async Task CreateCard_ShouldCallCreateCardInBlOnce()
        {
            //Arrange
            CardViewModel cardVM = new CardViewModel
            {
                Description = "Description",
            };
            var mockWallet = new Mock<IWalletWebService>();
            var mockCard = new Mock<ICardWebService>();
            CardController controller = new CardController(mockCard.Object, mockWallet.Object);
            mockCard.Setup(x => x.Create(It.IsAny<CardViewModel>())).ReturnsAsync(It.IsAny<CardViewModel>());

            //Act
            await controller.Create(cardVM);

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
        public async Task UpdateСard_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var walletId = 1;
            var card = new CardViewModel() { WalletId = walletId };
            var mockWallet = new Mock<IWalletWebService>();
            var mockСard = new Mock<ICardWebService>();
            var controller = new CardController(mockСard.Object, mockWallet.Object);
            mockСard.Setup(p => p.Update(walletId, It.IsAny<CardViewModel>()));

            // Act
            var result = await controller.Update(walletId, card);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
        }

        [Test]
        public async Task UpdateCard_NotNull_ViewResultIsNotNull()
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
            var result = await controller.Update(card.WalletId, card);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Details_ViewResultNotNull()
        {
            //Arrange
            var mockWallet = new Mock<IWalletWebService>();
            var mockСard = new Mock<ICardWebService>();
            int id = 1;
            mockСard.Setup(a => a.Get(id));
            var controller = new CardController(mockСard.Object, mockWallet.Object);

            //Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Details_CardExists_ReturnsAViewResultWithCard()
        {
            //Arrange
            var walletId = 1;
            var testCard = new CardViewModel() { WalletId = walletId };
            var mockCard = new Mock<ICardWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            mockCard.Setup(g => g.Get(walletId)).ReturnsAsync(testCard);
            var controller = new CardController(mockCard.Object, mockWallet.Object);

            // Act
            var result = await controller.Details(walletId);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as CardViewModel;
            Assert.AreEqual(walletId, model.WalletId);
        }

        [Test]
        public async Task Details_VerifyOnce()
        {
            //Arrange
            var WalletId = 1;
            var mockCard = new Mock<ICardWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new CardController(mockCard.Object, mockWallet.Object);

            //Act
            RedirectToActionResult result = await controller.Details(1) as RedirectToActionResult;

            //Assert
            mockCard.Verify(a => a.Get(WalletId), Times.Once);
        }
        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            //Arrange
            var mockCard = new Mock<ICardWebService>();
            var mockWallet = new Mock<IWalletWebService>();

            var controller = new CardController(mockCard.Object, mockWallet.Object);

            //Act
            var result = controller.RedirectToAction("Delete");

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }
        [Test]
        public async Task DeleteShouldReturnViewResult()
        {
            // Arrange
            int id = 1;
            var mockCard = new Mock<ICardWebService>();
            var mockWallet = new Mock<IWalletWebService>();

            var controller = new CardController(mockCard.Object, mockWallet.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }


        [Test]
        public async Task Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var card = new WalletViewModel();
            var mockCard = new Mock<ICardWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            mockCard.Setup(g => g.Delete(card.Id));
            var controller = new CardController(mockCard.Object, mockWallet.Object);

            //Act
            ViewResult result = await controller.Delete(card.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
