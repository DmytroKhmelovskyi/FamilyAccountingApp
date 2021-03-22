using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.DAL.Repositories;
using FamilyAccounting.DAL.Entities;
using Moq;
using NUnit.Framework;
using System;
using FamilyAccounting.AutoMapper;

namespace FamilyAccounting.Tests.ServiceTests
{
    public class PersonServiceTests
    {
        [Test]
        public void PersonService_CreateAnObject()
        {
            //Arrange
            DbConfig dbConfig = new DbConfig();
            IPersonRepository personRepository = new PersonRepository(dbConfig);
            var mock = new Mock<IMapper>();
            string expected = "PersonService";
      
            //Act
            PersonService personService = new PersonService(personRepository, mock.Object);

            //Assert
            Assert.IsNotNull(personService);
            Assert.AreEqual(expected, personService.GetType().Name);
        }

        [Test]
        public void GetListOfPersons_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IPersonService>();

            //Act
            mock.Setup(a => a.Get()).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Get(), Throws.Exception);
        }

        [Test]
        public void PersonService_UpdatePerson_ThrowsException()
        {
            //Arrange
            PersonDTO person = new PersonDTO()
            {
                Id = 1,
                FirstName = "new",
                LastName = "person"
            };
            var personId = person.Id;
            var mock = new Mock<IPersonService>();

            //Act
            mock.Setup(a => a.Update(personId, person)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Update(personId, person), Throws.Exception);
        }

        [Test]
        public void PersonService_Verify_UpdatingCalledOnce()
        {
            //Arrange
            PersonDTO person = new PersonDTO()
            {
                Id = 1,
                FirstName = "new",
                LastName = "person"
            };
            var personId = person.Id;
            var serviceMock = new Mock<IPersonService>();

            //Act
            serviceMock.Object.Update(personId, person);

            //Assert
            serviceMock.Verify(m => m.Update(personId, person), Times.Once);
        }

        [Test]
        public void PersonService_Verify_GetListOfPersonsCalledOnce()
        {
            //Arrange
            var serviceMock = new Mock<IPersonService>();

            //Act
            serviceMock.Object.Get();

            //Assert
            serviceMock.Verify(m => m.Get(), Times.Once);
        }

        [Test]
        public void PersonService_UpdatingPerson_ShouldNotNull()
        {
            //Arrange
            PersonDTO person = new PersonDTO()
            {
                Id = 1,
                FirstName = "new",
                LastName = "person"
            };
            var personId = person.Id;
            var serviceMock = new Mock<IPersonService>();

            //Act
            var result = serviceMock.Setup(a => a.Update(personId, person));

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void AddShouldCallAddInDalOnce()
        {
            //Arrange
            PersonDTO personDTO = new PersonDTO
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mockRepository = new Mock<IPersonRepository>();
            var mockMapper = new Mock<IMapper>();
            IPersonService service = new PersonService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.Add(It.IsAny<Person>())).Returns(It.IsAny<Person>());

            //Act
            service.Add(personDTO);

            //Assert
            mockRepository.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        }

        [Test]
        public void AddShouldReturnPersonDTO()
        {
            //Arrange
            PersonDTO shouldBe = new PersonDTO
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            PersonDTO personDTO = new PersonDTO
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(x => x.Add(personDTO)).Returns(personDTO);

            //Act
            var result = mock.Object.Add(personDTO);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(shouldBe.GetType().Name, result.GetType().Name);
        }

        [Test]
        public void PersonService_GetListOfPersons_ShouldNotNull()
        {
            //Arrange
            var serviceMock = new Mock<IPersonService>();

            //Act
            var result = serviceMock.Setup(a => a.Get());

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetSinglePerson_Success_IsNotNull()
        {
            //Arrenge
            var personDTO = new PersonDTO()
            {
                Id = 1
            };
            var personRepoMock = new Mock<IPersonRepository>();
            personRepoMock.Setup(r => r.Get(It.IsAny<int>())).Returns(new Person());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var personService = new PersonService(personRepoMock.Object, mapper);

            //Act
            var result = personService.Get(personDTO.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteShouldCallDeleteInDalOnce()
        {
            //Arrange
            var mockRepository = new Mock<IPersonRepository>();
            var mockMapper = new Mock<IMapper>();
            IPersonService service = new PersonService(mockRepository.Object, mockMapper.Object);
            mockRepository.Setup(x => x.Delete(1)).Returns(It.IsAny<int>);

            //Act
            service.Delete(1);

            //Assert
            mockRepository.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void PersonService_DeletePerson_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IPersonService>();

            //Act
            mock.Setup(a => a.Delete(1)).Throws(new Exception("Test Exception"));

            //Assert
            Assert.That(() => mock.Object.Delete(1), Throws.Exception);
        }
    }
}
