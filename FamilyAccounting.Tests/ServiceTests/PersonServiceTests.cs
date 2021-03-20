using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ServiceTests
{
    public class PersonServiceTests
    {
        [Test]
        public void PersonService_Verify_UpdatingCalledOnce()
        {
            //arrange
            PersonDTO person = new PersonDTO()
            {
                FirstName = "new",
                LastName = "person"
            };
            int id = 1;
            var serviceMock = new Mock<IPersonService>();

            //act
            serviceMock.Object.Update(id,person);

            //assert
            serviceMock.Verify(m => m.Update(id, person), Times.Once);
        }

        [Test]
        public void PersonService_UpdatingPerson_ShouldNotNull()
        {
            //arrange
            PersonDTO person = new PersonDTO()
            {
                FirstName = "new",
                LastName = "person"
            };
            int id = 1;
            var serviceMock = new Mock<IPersonService>();

            //act
            var result = serviceMock.Setup(a => a.Update(id, person));

            //assert
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
    }
}
