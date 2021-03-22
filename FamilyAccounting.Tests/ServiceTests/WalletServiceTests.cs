using AutoMapper;
using FamilyAccounting.AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
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
            //Arrange
            DbConfig dbConfig = new DbConfig();
            IWalletRepository walletRepository = new WalletRepository(dbConfig);
            var mock = new Mock<IMapper>();
            string expected = "WalletService";

            //Act
            WalletService walletService = new WalletService(walletRepository, mock.Object);

            //Assert
            Assert.IsNotNull(walletService);
            Assert.AreEqual(expected, walletService.GetType().Name);
        }

        [Test]
        public void WalletService_GetWallet_ShouldNotNull()
        {
            //Arrange
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
            //Arrange
            var serviceMock = new Mock<IWalletService>();
            int id = 1;

            //Act
            serviceMock.Object.Get(id);

            //Assert
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

        [Test]
        public void CreateWalletShouldCallCreateInDalOnce()
        {
            //Arrange
            WalletDTO walletDTO = new WalletDTO
            {
                Description = "Description",
                Balance = 100
            };
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IWalletRepository>();
            IWalletService service = new WalletService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.Create(It.IsAny<Wallet>())).Returns(It.IsAny<Wallet>());

            //Act
            service.Create(walletDTO);

            //Assert
            mockRepository.Verify(x => x.Create(It.IsAny<Wallet>()), Times.Once);
        }

        [Test]
        public void CreateShouldReturnWalletDTO()
        {
            //Arrange
            WalletDTO shouldBe = new WalletDTO
            {
                Description = "Description",
                Balance = 100
            };
            WalletDTO walletDTO = new WalletDTO
            {
                Description = "Description",
                Balance = 100
            };
            var mock = new Mock<IWalletService>();
            mock.Setup(x => x.Create(walletDTO)).Returns(walletDTO);

            //Act
            var result = mock.Object.Create(walletDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shouldBe.GetType().Name, result.GetType().Name);
        }
    }
}
