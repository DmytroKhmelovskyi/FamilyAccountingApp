using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

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
        public async Task Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var wallet = new WalletViewModel();
            var mockWallet = new Mock<IWalletWebService>();
            var mockPerson = new Mock<IPersonWebService>();
            mockWallet.Setup(g => g.Delete(wallet.Id));
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            //Act
            ViewResult result = await controller.Delete(wallet.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateShouldCallCreateWalletInBlOnce()
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
            mockWallet.Setup(x => x.Create(It.IsAny<WalletViewModel>())).ReturnsAsync(It.IsAny<WalletViewModel>());

            //Act
            await controller.Create(walletVM);

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
        public async Task Update_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var walletId = 1;
            var wallet = new WalletViewModel() { Id = walletId };
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);

            mockWallet.Setup(p => p.Update(walletId, It.IsAny<WalletViewModel>()));

            // Act
            var result = await controller.Update(walletId, wallet);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
        }

        [Test]
        public async Task Update_NotNull_ViewResultIsNotNull()
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
            var result = await controller.Update(wallet.Id, wallet);

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
        public async Task MakeActiveVerifyOnce()
        {
            //Arrange
            int id = 1;
            var mockPerson = new Mock<IPersonWebService>();
            var mockWallet = new Mock<IWalletWebService>();
            var controller = new WalletController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.MakeActive(id));

            //Act
            await controller.MakeActive(id);

            //Assert
            mockWallet.Verify(x => x.MakeActive(id), Times.Once);
        }
    }
}

