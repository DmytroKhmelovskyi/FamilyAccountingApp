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
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var result = serviceMock.Setup(a => a.GetAsync(id));

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
            serviceMock.Object.GetAsync(id);

            //Assert
            serviceMock.Verify(m => m.GetAsync(id), Times.Once);
        }

        [Test]
        public void WalletService_GetWallet_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            int id = 0;
            //Act
            mock.Setup(a => a.GetAsync(id)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.GetAsync(id), Throws.Exception);
        }

        [Test]
        public async Task DeleteWallet_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            int status = 1;
            var walletViewModel = new WalletViewModel()
            {
                Id = 1
            };
            var walletRepoMock = new Mock<IWalletRepository>();
            walletRepoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var walletService = new WalletService(walletRepoMock.Object, mapper);

            //Act
            var result = await walletService.DeleteAsync(walletViewModel.Id);

            //Assert
            walletRepoMock.Verify(r => r.DeleteAsync(It.Is<int>(id => id == walletViewModel.Id)), Times.Once);
        }

        [Test]
        public async Task DeleteWallet_IsNotNull()
        {
            //Arrenge
            int status = 1;
            var walletViewModel = new WalletViewModel();
            var walletRepoMock = new Mock<IWalletRepository>();
            walletRepoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var walletService = new WalletService(walletRepoMock.Object, mapper);

            //Act
            var result = await walletService.DeleteAsync(walletViewModel.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateWalletShouldCallCreateInDalOnce()
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
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<Wallet>())).ReturnsAsync(It.IsAny<Wallet>());

            //Act
            await service.CreateAsync(walletDTO);

            //Assert
            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Wallet>()), Times.Once);
        }

        [Test]
        public async Task CreateShouldReturnWalletDTO()
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
            mock.Setup(x => x.CreateAsync(walletDTO)).ReturnsAsync(walletDTO);

            //Act
            var result = await mock.Object.CreateAsync(walletDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shouldBe.GetType().Name, result.GetType().Name);
        }

        [Test]
        public void WalletService_UpdatingWallet_ShouldNotNull()
        {
            //Arrange
            WalletDTO wallet = new WalletDTO()
            {
                Id = 1,
                Description = "for shopping",
            };

            var walletId = wallet.Id;
            var mock = new Mock<IWalletService>();

            //Act
            var result = mock.Setup(a => a.UpdateAsync(walletId.Value, wallet));


            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetTransactionsShouldNotBeNull()
        {
            //Arrange
            List<TransactionDTO> test = new List<TransactionDTO>();
            var mock = new Mock<IWalletService>();
            mock.Setup(x => x.GetTransactionsAsync(2)).ReturnsAsync(test);

            //Act
            IEnumerable<TransactionDTO> result = await mock.Object.GetTransactionsAsync(2);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void WalletService_Verify_UpdateWalletCalledOnce()
        {
            //Arrange
            WalletDTO wallet = new WalletDTO()
            {
                Id = 1,
                Description = "for shopping",
            };

            var walletId = wallet.Id;
            var mock = new Mock<IWalletService>();

            //Act
            mock.Object.UpdateAsync(walletId.Value, wallet);

            //Assert
            mock.Verify(m => m.UpdateAsync(walletId.Value, wallet), Times.Once);
        }

        [Test]
        public async Task GetTransactionsShouldCallGetTransactionsInDALOnce()
        {
            //Arrange
            List<Transaction> test = new List<Transaction>();
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IWalletRepository>();
            IWalletService service = new WalletService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.GetTransactionsAsync(2)).ReturnsAsync(test);

            //Act
            await service.GetTransactionsAsync(2);

            //Assert
            mockRepository.Verify(x => x.GetTransactionsAsync(2), Times.Once);
        }

        [Test]
        public async Task GetTransactionShouldReturnIEnumerableOfTransactionDTO()
        {
            //Arrange
            List<Transaction> test = new List<Transaction>();
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IWalletRepository>();
            IWalletService service = new WalletService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.GetTransactionsAsync(2)).ReturnsAsync(test);

            //Act
            IEnumerable<TransactionDTO> result = await service.GetTransactionsAsync(2);

            //Assert
            Assert.AreEqual("FamilyAccounting.BL.DTO.TransactionDTO[]", "" + result.GetType() + "");
        }

        [Test]
        public async Task GetTransactionsShouldCallGetTransactionsWithDateInDALOnce()
        {
            //Arrange
            List<Transaction> test = new List<Transaction>();
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IWalletRepository>();
            IWalletService service = new WalletService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.GetTransactionsAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(test);

            //Act
            await service.GetTransactionsAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            mockRepository.Verify(x => x.GetTransactionsAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Test]
        public async Task GetTransactionWithDateShouldReturnIEnumerableOfTransactionDTO()
        {
            //Arrange
            List<Transaction> test = new List<Transaction>();
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<IWalletRepository>();
            IWalletService service = new WalletService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.GetTransactionsAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(test);

            //Act
            IEnumerable<TransactionDTO> result = await service.GetTransactionsAsync(2, It.IsAny<DateTime>(), It.IsAny<DateTime>());

            //Assert
            Assert.AreEqual("FamilyAccounting.BL.DTO.TransactionDTO[]", "" + result.GetType() + "");
        }

        [Test]
        public void WalletService_Verify_MakeActive()
        {
            //Arrange
            var serviceMock = new Mock<IWalletService>();
            int id = 1;

            //Act
            serviceMock.Object.MakeActiveAsync(id);

            //Assert
            serviceMock.Verify(m => m.MakeActiveAsync(id), Times.Once);
        }

        [Test]
        public void WalletService_MakeActive_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IWalletService>();
            int id = 0;
            //Act
            mock.Setup(a => a.MakeActiveAsync(id)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.MakeActiveAsync(id), Throws.Exception);
        }

        [Test]
        public void GetListOfWallets_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IWalletService>();

            //Act
            mock.Setup(a => a.GetAsync()).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.GetAsync(), Throws.Exception);
        }

        [Test]
        public void WalletService_GetListOfWallets_ShouldNotNull()
        {
            //Arrange
            var serviceMock = new Mock<IWalletService>();

            //Act
            var result = serviceMock.Setup(a => a.GetAsync());

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
