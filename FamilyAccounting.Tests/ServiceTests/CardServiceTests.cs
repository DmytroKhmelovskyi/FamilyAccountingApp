using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Repositories;
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
    }
}
