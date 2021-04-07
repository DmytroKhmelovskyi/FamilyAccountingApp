using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class TransactionsControllerTests
    {
        [Test]
        public void Update_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.Update(transactionId, It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.Update(1, It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SetInitial_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.SetInitialBalance(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.SetInitialBalance(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public void MakeExpense_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeExpense(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.MakeExpense(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public void MakeIncome_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeIncome(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.MakeIncome(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public void MakeTransfer_StatusCodeShouldBeOk()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.MakeTransfer(It.IsAny<TransactionDTO>()));
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.MakeTransfer(It.IsAny<TransactionDTO>()) as OkResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Details_ShouldReturnTransactionDTO()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionDTO() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionService>();
            var mockWallets = new Mock<IWalletService>();
            mockTransactions.Setup(p => p.Get(1,1)).Returns(It.IsAny<TransactionDTO>());
            var controller = new TransactionsController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.Details(1, 1);

            // Assert
            Assert.AreEqual("ActionResult`1", result.GetType().Name);
            Assert.IsNotNull(result);
        }
    }
}

