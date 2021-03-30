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

        [Test]
        public void Update_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var transactionId = 1;
            var transaction = new TransactionViewModel() { Id = transactionId };
            var mockTransactions = new Mock<ITransactionWebService>();
            var mockWallets = new Mock<IWalletWebService>();
            mockTransactions.Setup(p => p.Update(transactionId, It.IsAny<TransactionViewModel>()));
            var controller = new TransactionController(mockTransactions.Object, mockWallets.Object);

            // Act
            var result = controller.Update(transactionId, transaction);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Details", redirectToActionResult.ActionName);
        }

        [Test]
        public void Update_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var transaction = new TransactionViewModel()
            {
                Id = 1,
                Description = "Transaction",
                CategoryId = 1
            };
            var mockTransactions = new Mock<ITransactionWebService>();
            var mockWallets = new Mock<IWalletWebService>();
            var controller = new TransactionController(mockTransactions.Object, mockWallets.Object);

            //Act
            var result = controller.Update(transaction.Id, transaction);

            //Assert
            Assert.IsNotNull(result);
        }
    }

}
