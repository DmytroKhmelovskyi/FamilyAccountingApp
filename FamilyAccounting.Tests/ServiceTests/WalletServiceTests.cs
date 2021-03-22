using AutoMapper;
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
    class WalletServiceTests
    {
        [Test]
        public void WalletService_CreateAnObject()
        {
            // arrange
            DbConfig dbConfig = new DbConfig();
            IWalletRepository walletRepository = new WalletRepository(dbConfig);
            var mock = new Mock<IMapper>();
            string expected = "WalletService";

            // act
            WalletService walletService = new WalletService(walletRepository, mock.Object);

            //assert
            Assert.IsNotNull(walletService);
            Assert.AreEqual(expected, walletService.GetType().Name);
        }

        [Test]
        public void WalletService_GetWallet_ShouldNotNull()
        {
            //arrange
            var serviceMock = new Mock<IWalletService>();
            int id = 1;

            //act
            var result = serviceMock.Setup(a => a.Get(id));

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void WalletService_Verify_GetWalletCalledOnce()
        {
            //arrange
            var serviceMock = new Mock<IWalletService>();
            int id = 1;

            //act
            serviceMock.Object.Get(id);

            //assert
            serviceMock.Verify(m => m.Get(id), Times.Once);
        }

        [Test]
        public void WalletService_GetWallet_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            int id = 0;
            //Act
            mock.Setup(a => a.Get(id)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Get(id), Throws.Exception);
        }
    }
}
