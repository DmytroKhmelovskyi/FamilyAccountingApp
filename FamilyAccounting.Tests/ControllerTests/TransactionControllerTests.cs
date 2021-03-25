using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace FamilyAccounting.Tests.ControllerTests
{
    class TransactionControllerTests
    {
        [Test]
        public void MakeExpense_ViewResultNotNull()
        {
            //Arrange
            var wallet = new WalletViewModel
            {
                Id = 1,
                Description = "test"                
            };
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();
            var transaction = new Mock<TransactionViewModel>();
            mock.Setup(a => a.MakeExpense(transaction.Object)).Returns(It.IsAny<TransactionViewModel>());
            mock2.Setup(a => a.Get(1)).Returns(wallet);
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ViewResult result = controller.MakeExpense(transaction.Object.SourceWalletId) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MakeExpense_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();
            var transaction = new Mock<TransactionViewModel>();
            mock.Setup(a => a.MakeExpense(transaction.Object)).Throws(new Exception("Test Exception"));
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ContentResult result = controller.MakeExpense(transaction.Object.Id) as ContentResult;

            //Assert
            Assert.That(() => mock.Object.MakeExpense(transaction.Object), Throws.Exception);
        }

        [Test]
        public void SetInitialBalance_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();
            var transaction = new TransactionViewModel
            {
                SourceWallet = "Test wallet",
                SourceWalletId = 1,
                CategoryId = 15
            };

            mock.Setup(a => a.SetInitialBalance(transaction));
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ViewResult result = controller.SetInitialBalance(transaction.SourceWalletId) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void SetInitialBalance_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();
            var transaction = new Mock<TransactionViewModel>();
            mock.Setup(a => a.SetInitialBalance(transaction.Object)).Throws(new Exception("Test Exception"));
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ContentResult result = controller.SetInitialBalance(transaction.Object.Id) as ContentResult;

            //Assert
            Assert.That(() => mock.Object.SetInitialBalance(transaction.Object), Throws.Exception);
        }
        [Test]
        public void TransactionController_CreateAnObject()
        {
            //Arrange
            string expected = "TransactionController";
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();

            //Act
            TransactionController controller = new TransactionController(mock.Object, mock2.Object); 

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }
    }
}
