using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.ControllerTests
{
    class WalletsControllerTests
    {
        [Test]
        public async Task Index_IsNotNull()
        {
            //Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            mockWallet.Setup(a => a.Get());

            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            //Act
            var result = await controller.Index() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void Index_VerifyOnce()
        {
            //Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            //Act
            var result = controller.Index();

            //Assert
            mockWallet.Verify(a => a.Get(), Times.Once);
        }

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
        public async Task AddShouldCallAddWalletOnce()
        {
            //Arrange
            WalletDTO walletDTO = new WalletDTO
            {
                Description = "Wallet",
                Balance = 500
            };
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller =  new WalletsController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.Create(It.IsAny<WalletDTO>())).ReturnsAsync(It.IsAny<WalletDTO>());

            //Act
            await controller.Create(walletDTO);

            //Assert
            mockWallet.Verify(x => x.Create(It.IsAny<WalletDTO>()), Times.Once);
        }

        [Test]
        public void AddShouldRedirectToActionCreate()
        {
            // Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Create");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Create"));
        }

        [Test]
        public async Task AddShouldReturnOkResult()
        {
            // Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);
            var walletDTO = new WalletDTO();
            // Act
            var result = await controller.Create(walletDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public async Task UpdateVerifyOnce()
        {
            //Arrange
            WalletDTO walletDTO = new WalletDTO
            {
                Id = 45,
                Description = "Wallet",
                Balance = 500
            };
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.Update((int)walletDTO.Id, walletDTO));

            //Act
            await controller.Update((int)walletDTO.Id, walletDTO);

            //Assert
            mockWallet.Verify(x => x.Update((int)walletDTO.Id, walletDTO));
        }

        [Test]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var walletId = 1;
            var wallet = new WalletDTO() { Id = walletId };
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            mockWallet.Setup(p => p.Update(walletId, It.IsAny<WalletDTO>()));
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = await controller.Update(walletId, wallet) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task Details_IsNotNull()
        {
            //Arrange
            var walletId = 1;
            var testWallet = new WalletDTO() { Id = walletId };
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            mockWallet.Setup(g => g.Get(walletId)).ReturnsAsync(testWallet);
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = await controller.Details(walletId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task DeleteShouldCallDeleteWalletInBlOnce()
        {
            //Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);
            mockWallet.Setup(x => x.Delete(1));

            //Act
            await controller.DeleteWallet(1);

            //Assert
            mockWallet.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            // Arrange
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = controller.RedirectToAction("Delete");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }

        [Test]
        public async Task DeleteShouldReturnActionResult()
        {
            // Arrange
            int id = 1;
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = await controller.Delete(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public async Task DeleteWalletShouldReturnActionResult()
        {
            // Arrange
            int id = 1;
            var mockWallet = new Mock<IWalletService>();
            var mockPerson = new Mock<IPersonService>();
            WalletsController controller = new WalletsController(mockWallet.Object, mockPerson.Object);

            // Act
            var result = await controller.DeleteWallet(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
