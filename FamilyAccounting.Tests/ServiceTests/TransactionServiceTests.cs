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
            serviceMock.Object.MakeExpenseAsync(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.MakeExpenseAsync(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_MakeExpense_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            mock.Setup(a => a.MakeExpenseAsync(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeExpenseAsync(transactionDTO.Object), Throws.Exception);
        }

        [Test]
        public void TransactionService_Verify_SetInitialBalanceCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            serviceMock.Object.SetInitialBalanceAsync(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.SetInitialBalanceAsync(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_SetInitialBalance_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();

            //Act
            mock.Setup(a => a.SetInitialBalanceAsync(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.SetInitialBalanceAsync(transactionDTO.Object), Throws.Exception);
        }


        [Test]
        public void TransactionService_Verify_MakeIncomeCalledOnce()
          {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();
             //Act
            serviceMock.Object.MakeIncomeAsync(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.MakeIncomeAsync(transactionDTO.Object), Times.Once);
        }

        [Test]
        public void TransactionService_Verify_MakeTransferCalledOnce()
          {
            //Arrange
            var serviceMock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();
  
            //Act                  
            serviceMock.Object.MakeTransferAsync(transactionDTO.Object);

            //Assert
            serviceMock.Verify(m => m.MakeTransferAsync(transactionDTO.Object), Times.Once);
        }    

        [Test]
        public void TransactionService_MakeIncome_ThrowsException()
             {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();
          
             //Act
            mock.Setup(a => a.MakeIncomeAsync(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeIncomeAsync(transactionDTO.Object), Throws.Exception);
        }

        [Test]
        public void TransactionService_MakeTransfer_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ITransactionService>();
            var transactionDTO = new Mock<TransactionDTO>();
          
          //Act
            mock.Setup(a => a.MakeTransferAsync(transactionDTO.Object)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeTransferAsync(transactionDTO.Object), Throws.Exception);
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
            mock.Setup(a => a.UpdateAsync(transactionId, transaction)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.UpdateAsync(transactionId, transaction), Throws.Exception);
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
            mock.Object.UpdateAsync(transactionId, transaction);

            //Assert
            mock.Verify(m => m.UpdateAsync(transactionId, transaction), Times.Once);
        }
    }
}
