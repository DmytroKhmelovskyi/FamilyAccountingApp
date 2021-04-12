using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.Integration_Tests
{
    public class WalletsControllerTests : TestClientProvider
    {
        [Test]
        public async Task Test_AddWallet_ReturnsOk()
        {
            // Arrange
            var newWallet = new WalletDTO {
                Id = 45,
                Description = "Wallet",
                Balance = 500
            };
            WalletRepoMock.Setup(r => r.Create(It.IsAny<Wallet>())).ReturnsAsync(new Wallet());
            var newWalletJson = JsonConvert.SerializeObject(newWallet);
            var content = new StringContent(newWalletJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/wallets/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            WalletRepoMock.Verify(r => r.Create(It.IsAny<Wallet>()), Times.Once);
        }

        [Test]
        public async Task Test_UpdateWallet_ReturnsOk()
        {
            // Arrange
            var walletId = 1;
            var newWallet = new WalletDTO
            {
                Id = walletId,
                Description = "Wallet",
                Balance = 500
            };
            WalletRepoMock.Setup(r => r.Update(walletId, It.IsAny<Wallet>())).ReturnsAsync(new Wallet());
            var newWalletJson = JsonConvert.SerializeObject(newWallet);
            var content = new StringContent(newWalletJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"/api/wallets/Update/{walletId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            WalletRepoMock.Verify(r => r.Update(walletId, It.IsAny<Wallet>()), Times.Once);
        }

        [Test]
        public async Task Test_DeleteWallet_ReturnsOk()
        {
            // Arrange
            var walletId = 1;
            WalletRepoMock.Setup(r => r.Delete(walletId)).ReturnsAsync(0);

            // Act
            var response = await Client.DeleteAsync($"/api/wallets/Delete/{walletId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            WalletRepoMock.Verify(r => r.Delete(walletId), Times.Once);
        }


        [Test]
        public async Task Test_DetailsWallet_ReturnsOk()
        {
            // Arrange
            var walletId = 1;
            var transactionId = 2;
            WalletRepoMock.Setup(r => r.Get(walletId)).ReturnsAsync(new Wallet {
                Id = walletId,
                Description = "Wallet",
                Balance = 500
            });
            WalletRepoMock.Setup(r => r.GetTransactions(walletId)).ReturnsAsync(new List<Transaction>() { new Transaction { Id = transactionId } });

            // Act
            var response = await Client.GetAsync($"/api/wallets/Details/{walletId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var wallet = JsonConvert.DeserializeObject<WalletDTO>(content);
            Assert.AreEqual(walletId, wallet.Id);
            Assert.AreEqual(1, wallet.Transactions.Count());
            Assert.AreEqual(transactionId, wallet.Transactions.ToList().FirstOrDefault().Id);
        }
    }
}
