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
    public class PersonsControllerTests : TestClientProvider
    {
        [Test]
        public async Task Test_Get_AllPersons()
        {
            // Arrange
            var personsList = new List<Person>() { new Person { }, new Person { } };
            PersonRepoMock.Setup(r => r.Get()).Returns(personsList);

            // Act
            var response = await Client.GetAsync("/api/persons/index");

            // Assert
            response.EnsureSuccessStatusCode();
            var personsJson = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<PersonDTO>>(personsJson);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(personsList.Count, persons.Count);
        }

        [Test]
        public async Task Test_AddPerson_ReturnsOk()
        {
            // Arrange
            var newPerson = new PersonDTO { FirstName = "A", LastName = "B", Phone = "324234", Email = "sdf@f.com" };

            PersonRepoMock.Setup(r => r.Add(It.IsAny<Person>())).Returns(new Person());
            var newPersonJson = JsonConvert.SerializeObject(newPerson);
            var content = new StringContent(newPersonJson, Encoding.UTF8, "application/json");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await Client.PostAsync("/api/persons/Add", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            PersonRepoMock.Verify(r => r.Add(It.IsAny<Person>()), Times.Once);
        }

        [Test]
        public async Task Test_UpdatePerson_ReturnsOk()
        {
            // Arrange
            var personId = 1;
            var person = new PersonDTO { FirstName = "A", LastName = "B", Phone = "324234", Email = "sdf@f.com" };

            PersonRepoMock.Setup(r => r.Update(personId, It.IsAny<Person>())).Returns(new Person());
            var newPersonJson = JsonConvert.SerializeObject(person);
            var content = new StringContent(newPersonJson, Encoding.UTF8, "application/json");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await Client.PutAsync($"/api/persons/Update/{personId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            PersonRepoMock.Verify(r => r.Update(personId, It.IsAny<Person>()), Times.Once);
        }

        [Test]
        public async Task Test_DeletePerson_ReturnsOk()
        {
            // Arrange
            var personId = 1;

            PersonRepoMock.Setup(r => r.Delete(personId)).Returns(0);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await Client.DeleteAsync($"/api/persons/Delete/{personId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            PersonRepoMock.Verify(r => r.Delete(personId), Times.Once);
        }

        [Test]
        public async Task Test_DetailsPerson_ReturnsOk()
        {
            // Arrange
            var personId = 1;
            var walletId = 2;

            PersonRepoMock.Setup(r => r.Get(personId)).Returns(new Person { Id = personId, FirstName = "A", LastName = "B" });
            PersonRepoMock.Setup(r => r.GetWallets(personId)).Returns(new List<Wallet>() { new Wallet { Id = walletId } });
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await Client.GetAsync($"/api/persons/Details/{personId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<PersonDTO>(content);
            Assert.AreEqual(personId, person.Id);
            Assert.AreEqual(1, person.Wallets.Count());
            Assert.AreEqual(walletId, person.Wallets.ToList().FirstOrDefault().Id);
        }
    }
}
