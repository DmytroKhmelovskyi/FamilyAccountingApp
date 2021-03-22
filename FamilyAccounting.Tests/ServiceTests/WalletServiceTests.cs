using AutoMapper;
using FamilyAccounting.AutoMapper;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Repositories;
using FamilyAccounting.Web.Models;
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

        [Test]
        public void DeleteGuest_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            int status = 1;
            var walletViewModel = new WalletViewModel()
            {
                Id = 1
            };
            var walletRepoMock = new Mock<IWalletRepository>();
            walletRepoMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var walletService = new WalletService(walletRepoMock.Object, mapper);

            //Act
            var result = walletService.Delete(walletViewModel.Id);

            //Assert
            walletRepoMock.Verify(r => r.Delete(It.Is<int>(id => id == walletViewModel.Id)), Times.Once);
        }
        [Test]
        public void DeleteGuest_IsNotNull()
        {
            //Arrenge
            int status = 1;
            var walletViewModel = new WalletViewModel();
            var walletRepoMock = new Mock<IWalletRepository>();
            walletRepoMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var walletService = new WalletService(walletRepoMock.Object, mapper);

            //Act
            var result = walletService.Delete(walletViewModel.Id);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
