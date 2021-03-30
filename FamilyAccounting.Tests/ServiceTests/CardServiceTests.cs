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
using System.Text;

namespace FamilyAccounting.Tests.ServiceTests
{
    public class CardServiceTests
    {

        [Test]
        public void CardService_CreateAnObject()
        {
            //Arrange
            DbConfig dbConfig = new DbConfig();
            ICardRepository cardRepository = new CardRepository(dbConfig);
            var mock = new Mock<IMapper>();
            string expected = "CardService";

            //Act
            CardService cardService = new CardService(mock.Object, cardRepository);

            //Assert
            Assert.IsNotNull(cardService);
            Assert.AreEqual(expected, cardService.GetType().Name);
        }

        [Test]
        public void CreateCard_ShouldCallCreateInDalOnce()
        {
            //Arrange
            CardDTO cardDTO = new CardDTO
            {
                Description = "Description",
            };
            var mockMapper = new Mock<IMapper>();
            var mockRepository = new Mock<ICardRepository>();
            ICardService service = new CardService(mockMapper.Object, mockRepository.Object);
            mockRepository.Setup(x => x.Create(It.IsAny<Card>())).Returns(It.IsAny<Card>());

            //Act
            service.Create(cardDTO);

            //Assert
            mockRepository.Verify(x => x.Create(It.IsAny<Card>()), Times.Once);
        }

        [Test]
        public void CreateCard_ShouldReturnCardDTO()
        {
            //Arrange
            CardDTO shouldBe = new CardDTO
            {
                Description = "Description",
            };

            CardDTO cardDTO = new CardDTO
            {
                Description = "Description",
            };
            var mock = new Mock<ICardService>();
            mock.Setup(x => x.Create(cardDTO)).Returns(cardDTO);

            //Act
            var result = mock.Object.Create(cardDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shouldBe.GetType().Name, result.GetType().Name);
        }

        [Test]
        public void CardService_UpdateCard_ShouldNotBeNull()
        {
            //Arrange
            CardDTO card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var cardId = card.WalletId;
            var mock = new Mock<ICardService>();

            //Act
            var result = mock.Setup(a => a.Update(cardId, card));


            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void CardService_Verify_UpdateCardCalledOnce()
        {
            //Arrange
            CardDTO card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var cardId = card.WalletId;
            var mock = new Mock<ICardService>();

            //Act
            mock.Object.Update(cardId, card);

            //Assert
            mock.Verify(m => m.Update(cardId, card), Times.Once);
        }

        [Test]
        public void CardService_UpdateCard_ThrowsException()
        {
            //Arrange
            CardDTO card = new CardDTO()
            {
                WalletId = 1,
                Description = "for shopping",
            };
            var cardId = card.WalletId;
            var mock = new Mock<ICardService>();

            //Act
            mock.Setup(a => a.Update(cardId, card)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Update(cardId, card), Throws.Exception);
        }

        [Test]
        public void CardService_GetCard_ShouldNotBeNull()
        {
            //Arrange
            var serviceMock = new Mock<ICardService>();
            int id = 1;

            //act
            var result = serviceMock.Setup(a => a.Get(id));

            //assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CardService_GetCard_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ICardService>();
            int id = 0;
            //Act
            mock.Setup(a => a.Get(id)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Get(id), Throws.Exception);
        }

        [Test]
        public void CardtService_Verify_GetCardCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<ICardService>();
            int id = 1;

            //Act
            serviceMock.Object.Get(id);

            //Assert
            serviceMock.Verify(m => m.Get(id), Times.Once);
        }

        [Test]
        public void DeleteShouldCallDeleteInDalOnce()
        {
            //Arrange
            var mockRepository = new Mock<ICardRepository>();
            var mockMapper = new Mock<IMapper>();
            ICardService service = new CardService(mockMapper.Object, mockRepository.Object);
            mockRepository.Setup(x => x.Delete(1)).Returns(It.IsAny<int>);

            //Act
            service.Delete(1);

            //Assert
            mockRepository.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void CardService_DeleteCard_ThrowsException()
        {
            //Arrange
            var mock = new Mock<ICardService>();

            //Act
            mock.Setup(a => a.Delete(1)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Delete(1), Throws.Exception);
        }
        [Test]
        public void DeleteCard_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            int status = 1;
            var cardViewModel = new CardViewModel()
            {
                WalletId = 1
            };
            var cardRepoMock = new Mock<ICardRepository>();
            cardRepoMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var cardService = new CardService(mapper, cardRepoMock.Object);

            //Act
            var result = cardService.Delete(cardViewModel.WalletId);

            //Assert
            cardRepoMock.Verify(r => r.Delete(It.Is<int>(id => id == cardViewModel.WalletId)), Times.Once);
        }

        [Test]
        public void DeleteCard_IsNotNull()
        {
            //Arrenge
            int status = 1;
            var cardViewModel = new CardViewModel();
            var cardRepoMock = new Mock<ICardRepository>();
            cardRepoMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(status);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var walletService = new CardService(mapper, cardRepoMock.Object);

            //Act
            var result = walletService.Delete(cardViewModel.WalletId);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
