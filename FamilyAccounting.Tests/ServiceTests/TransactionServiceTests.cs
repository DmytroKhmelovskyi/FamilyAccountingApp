﻿using AutoMapper;
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
    }
}