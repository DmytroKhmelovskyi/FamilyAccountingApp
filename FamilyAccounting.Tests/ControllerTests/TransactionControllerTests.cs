using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

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
            var wallet = new WalletViewModel
            {
                Id=1,
                Description="Details"
            };
            var transaction = new TransactionViewModel
            {
                SourceWalletId = wallet.Id,
                SourceWallet = wallet.Description
            };
            TransactionController controller = new TransactionController(mock.Object, mock2.Object);

            //Act
            ViewResult result = controller.MakeExpense(transaction.SourceWalletId) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
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
