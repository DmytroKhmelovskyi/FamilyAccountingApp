using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.Integration_Tests
{
    public class TransactionsControllerTests : TestClientProvider
    {
        [Test]
        public async Task Test_DetailsTransaction()
        {
            // Arrange
            int walletId = 1;
            int transactionId = 1;
            var transaction = new Transaction { SourceWalletId = walletId, Amount = 433, Description = "transact" };
            TransactionRepoMock.Setup(r => r.GetAsync(walletId, transactionId)).ReturnsAsync(transaction);

            // Act
            var response = await Client.GetAsync($"/api/transactions/details/1?walletId=1&transactionId=1");

            // Assert
            response.EnsureSuccessStatusCode();
            var transactionJson = await response.Content.ReadAsStringAsync();
            var trans = JsonConvert.DeserializeObject<TransactionDTO>(transactionJson);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.AreEqual(trans, transaction);
            Assert.AreEqual(walletId, transaction.SourceWalletId);
        }

        [Test]
        public async Task Test_MakeExpenseOk()
        {
            // Arrange
            int walletId = 1;
            var transaction = new Transaction { SourceWalletId = walletId, Amount = 433, Description = "transact" };
            TransactionRepoMock.Setup(r => r.MakeExpenseAsync(transaction)).ReturnsAsync(transaction);
            var newTransactionJson = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(newTransactionJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/transactions/makeexpense", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            TransactionRepoMock.Verify(r => r.MakeExpenseAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public async Task Test_MakeIncomeOk()
        {
            // Arrange
            int walletId = 1;
            var transaction = new Transaction { TargetWalletId = walletId, Amount = 433, Description = "transact" };
            TransactionRepoMock.Setup(r => r.MakeIncomeAsync(transaction)).ReturnsAsync(transaction);
            var newTransactionJson = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(newTransactionJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/transactions/makeincome", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            TransactionRepoMock.Verify(r => r.MakeIncomeAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public async Task Test_MakeTransferOk()
        {
            // Arrange
            int walletSourseId = 1;
            int walletTaretId = 1;
            var transaction = new Transaction { TargetWalletId = walletTaretId, SourceWalletId = walletSourseId, Amount = 433, Description = "transact" };
            TransactionRepoMock.Setup(r => r.MakeTransferAsync(transaction)).ReturnsAsync(transaction);
            var newTransactionJson = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(newTransactionJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/transactions/maketransfer", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            TransactionRepoMock.Verify(r => r.MakeTransferAsync(It.IsAny<Transaction>()), Times.Once);

        }

        [Test]
        public async Task Test_UpdateTransaction_ReturnsOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO { Id = transactionId, Description = "desc" };
            TransactionRepoMock.Setup(r => r.UpdateAsync(transactionId, It.IsAny<Transaction>())).ReturnsAsync(new Transaction());
            var newTransactionJson = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(newTransactionJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"/api/transactions/Update/{transactionId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            TransactionRepoMock.Verify(r => r.UpdateAsync(transactionId, It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public async Task Test_SetInitialBalanceOk()
        {
            // Arrange
            int walletSourseId = 1;
            var transaction = new Transaction { SourceWalletId = walletSourseId, Amount = 433, Description = "transact" };
            TransactionRepoMock.Setup(r => r.SetInitialBalanceAsync(transaction)).ReturnsAsync(transaction);
            var newTransactionJson = JsonConvert.SerializeObject(transaction);
            var content = new StringContent(newTransactionJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/transactions/setinitialbalance", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            TransactionRepoMock.Verify(r => r.SetInitialBalanceAsync(It.IsAny<Transaction>()), Times.Once);

        }
    }
}
