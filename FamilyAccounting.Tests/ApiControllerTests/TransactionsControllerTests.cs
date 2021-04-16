using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.ControllerTests
{
    class TransactionsControllerTests
    {
        [Test]
        public async Task Update_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.UpdateAsync(transactionId, It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.Update(1, It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task SetInitial_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.SetInitialBalanceAsync(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.SetInitialBalance(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MakeExpense_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeExpenseAsync(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.MakeExpense(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MakeIncome_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeIncomeAsync(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.MakeIncome(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task MakeTransfer_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeTransferAsync(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.MakeTransfer(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Details_ShouldReturnTransactionDTO()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.GetAsync(1, 1)).ReturnsAsync(It.IsAny<TransactionDTO>());
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.Details(1, 1);

            // Assert
            Assert.AreEqual("ActionResult`1", result.GetType().Name);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Details_ShouldReturnData()
        {
            // Arrange
            TransactionDTO transaction = new TransactionDTO
            {
                Id = 159,
                Amount = 92,
                SourceWalletId = 14,
                SourceWallet = "Cash X",
                TargetWalletId = 22,
                TargetWallet = "Reserve funds S",
                Description = "transfer act descr  J C U C B E E R L S",
                TimeStamp = new DateTime(2019, 04, 07, 15, 15, 22),
                CategoryId = 0,
                Category = "",
                State = false,
                TransactionType = (TransactionTypeDTO)1,
                BalanceBefore = 36,
                BalanceAfter = 36
            };
            string shuoldBe = JsonConvert.SerializeObject(transaction);
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.GetAsync(14, 159)).ReturnsAsync(transaction);
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = await controller.Details(14, 159);
            string output = JsonConvert.SerializeObject(result.Value);

            // Assert
            Assert.AreEqual(shuoldBe, output);
            Assert.IsNotNull(result);
        }
    }
}

