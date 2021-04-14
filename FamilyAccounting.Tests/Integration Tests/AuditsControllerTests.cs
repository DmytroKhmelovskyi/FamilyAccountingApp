using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests
{
    public class AuditsControllerTests : TestClientProvider
    {
        [Test]
        public async Task Test_GetPersons()
        {
            // Arrange
            var personsList = new List<AuditPerson>() { new AuditPerson { }, new AuditPerson { } };
            AuditRepoMock.Setup(r => r.GetPersonsAsync()).ReturnsAsync(personsList);

            // Act
            var response = await Client.GetAsync("/api/audits/getpersons");

            // Assert
            response.EnsureSuccessStatusCode();
            var personsJson = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<AuditPersonDTO>>(personsJson);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(personsList.Count, persons.Count);
            AuditRepoMock.Verify(r => r.GetPersonsAsync(), Times.Once);
        }


        [Test]
        public async Task Test_GetWallets()
        {
            // Arrange
            var auditsList = new List<AuditWallet>() { new AuditWallet { }, new AuditWallet { } };
            AuditRepoMock.Setup(r => r.GetWalletsAsync()).ReturnsAsync(auditsList);

            // Act
            var response = await Client.GetAsync("/api/audits/getwallets");

            // Assert
            response.EnsureSuccessStatusCode();
            var auditsJson = await response.Content.ReadAsStringAsync();
            var audits = JsonConvert.DeserializeObject<List<AuditWalletDTO>>(auditsJson);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(auditsList.Count, audits.Count);
            AuditRepoMock.Verify(r => r.GetWalletsAsync(), Times.Once);
        }

        [Test]
        public async Task Test_GetActions()
        {
            // Arrange
            var actionsList = new List<AuditAction>() { new AuditAction { }, new AuditAction { } };
            AuditRepoMock.Setup(r => r.GetActionsAsync()).ReturnsAsync(actionsList);

            // Act
            var response = await Client.GetAsync("/api/audits/getactions");

            // Assert
            response.EnsureSuccessStatusCode();
            var actionsJson = await response.Content.ReadAsStringAsync();
            var actions = JsonConvert.DeserializeObject<List<PersonDTO>>(actionsJson);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(actionsList.Count, actions.Count);
            AuditRepoMock.Verify(r => r.GetActionsAsync(), Times.Once);
        }
    }
}