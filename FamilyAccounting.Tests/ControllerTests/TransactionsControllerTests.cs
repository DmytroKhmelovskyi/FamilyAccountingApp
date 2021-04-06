using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class TransactionsControllerTests
    {
        [Test]
        public void TransactionController_CreateAnObject()
        {
            //Arrange
            string expected = "TransactionsController";
            var mock = new Mock<ITransactionService>();
            var mock2 = new Mock<IWalletService>();

            //Act
            TransactionsController controller = new TransactionsController(mock.Object, mock2.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }  
    }
}

