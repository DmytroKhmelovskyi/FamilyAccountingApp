using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System;

namespace FamilyAccounting.Tests.ServiceTests
{
    class TransactionServiceTests
    {
        [Test]
        public void TransactionService_CreateAnObject()
        {
            //Arrange
            DbConfig dbConfig = new DbConfig();
            ITransactionRepository transactionRepository = new TransactionRepository(dbConfig);
            var mock = new Mock<IMapper>();
            string expected = "TransactionService";

            //Act
            TransactionService transactionService = new TransactionService(transactionRepository, mock.Object);

            //Assert
            Assert.IsNotNull(transactionService);
            Assert.AreEqual(expected, transactionService.GetType().Name);
        }
        [Test]
        public void TransactionService_Verify_MakeExpenseCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            serviceMock.Object.MakeExpense(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.MakeExpense(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_MakeExpense_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            mock.Setup(a => a.MakeExpense(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeExpense(transactionDTO.Object), Throws.Exception);
        }

        [Test]
        public void TransactionService_Verify_SetInitialBalanceCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            serviceMock.Object.SetInitialBalance(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.SetInitialBalance(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_SetInitialBalance_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            mock.Setup(a => a.SetInitialBalance(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.SetInitialBalance(transactionDTO.Object), Throws.Exception);
        }

        [Test]
        public void TransactionService_Verify_MakeIncomeCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            serviceMock.Object.MakeIncome(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.MakeIncome(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_MakeIncome_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            mock.Setup(a => a.MakeIncome(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeIncome(transactionDTO.Object), Throws.Exception);
        }

        [Test]
        public void TransactionService_UpdatePerson_ThrowsException()
        {
            //Arrange
            TransactionDTO transaction = new TransactionDTO()
            {
                Id = 1,
                Description = "new",
                CategoryId = 2
            };
            var transactionId = transaction.Id;
            var mock = new Mock<ITransactionService>();

            //Act
            mock.Setup(a => a.Update(transactionId, transaction)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Update(transactionId, transaction), Throws.Exception);
        }

        [Test]
        public void TransactionService_Verify_UpdatingCalledOnce()
        {
            //Arrange
            TransactionDTO transaction = new TransactionDTO()
            {
                Id = 1,
                Description = "new",
                CategoryId = 2
            };
            var transactionId = transaction.Id;
            var mock = new Mock<ITransactionService>();

            //Act
            mock.Object.Update(transactionId, transaction);

            //Assert
            mock.Verify(m => m.Update(transactionId, transaction), Times.Once);
        }
    }
}
