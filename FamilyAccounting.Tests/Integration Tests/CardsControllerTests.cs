using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.Integration_Tests
{
    public class CardsControllerTests : TestClientProvider
    {
        [Test]
        public async Task Test_AddCard_ReturnsOk()
        {
            // Arrange
            var newCard = new CardDTO {  WalletId = 1, Description = "for shopping"};

            CardRepoMock.Setup(r => r.CreateAsync(It.IsAny<Card>())).ReturnsAsync(new Card());
            var newCardJson = JsonConvert.SerializeObject(newCard);
            var content = new StringContent(newCardJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync("/api/cards/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CardRepoMock.Verify(r => r.Create(It.IsAny<Card>()), Times.Once);
        }

        [Test]
        public async Task Test_UpdateCard_ReturnsOk()
        {
            // Arrange
            var walletId = 1;
            var card = new CardDTO { WalletId = walletId, Description = "new description"};

            CardRepoMock.Setup(r => r.UpdateAsync(walletId, It.IsAny<Card>())).ReturnsAsync(new Card());
            var newCardJson = JsonConvert.SerializeObject(card);
            var content = new StringContent(newCardJson, Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"/api/cards/Update/{walletId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CardRepoMock.Verify(r => r.Update(walletId, It.IsAny<Card>()), Times.Once);
        }

        [Test]
        public async Task Test_DeleteCard_ReturnsOk()
        {
            // Arrange
            var walletId = 1;

            CardRepoMock.Setup(r => r.DeleteAsync(walletId)).ReturnsAsync(0);

            // Act
            var response = await Client.DeleteAsync($"/api/cards/Delete/{walletId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CardRepoMock.Verify(r => r.Delete(walletId), Times.Once);
        }

        [Test]
        public async Task Test_DetailsCard_ReturnsOk()
        {
            // Arrange
            var walletId = 1;
            CardRepoMock.Setup(r => r.GetAsync(walletId)).ReturnsAsync(new Card { WalletId = walletId, Description="for shopping", Number = "3566002020360505" });

            // Act
            var response = await Client.GetAsync($"/api/cards/Details/{walletId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var card = JsonConvert.DeserializeObject<CardDTO>(content);
            Assert.AreEqual(walletId, card.WalletId);
        }
    }
}
