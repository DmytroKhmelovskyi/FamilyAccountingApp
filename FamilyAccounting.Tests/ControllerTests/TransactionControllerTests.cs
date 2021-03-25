using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
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
            var mock = new Mock<ITransactionWebService>();
            var mock2 = new Mock<IWalletWebService>();
            var transaction = new Mock<TransactionViewModel>();

            mock.Setup(a => a.MakeExpense(transaction.Object));
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ViewResult result = controller.MakeExpense(transaction.Object.Id) as ViewResult;

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
