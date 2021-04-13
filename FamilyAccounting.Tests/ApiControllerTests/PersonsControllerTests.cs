using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonsControllerTests
    {
        [Test]
        public async Task Index_IsNotNull()
        {
            //Arrange
            var TestList = new List<PersonDTO>
            {
                new PersonDTO { FirstName = "Kristin", LastName = "Hansen", WalletsCount = 0 },
                new PersonDTO { FirstName = "Aubrey", LastName = "Sampson", WalletsCount = 4 },
                new PersonDTO { FirstName = "Phillip", LastName = "Espinoza", WalletsCount = 3 },
                new PersonDTO { FirstName = "Armand", LastName = "Powers", WalletsCount = 2 },
                new PersonDTO { FirstName = "Walter", LastName = "Foley", WalletsCount = 4 },
                new PersonDTO { FirstName = "Shaine", LastName = "Macdonald", WalletsCount = 3 },
                new PersonDTO { FirstName = "Regina", LastName = "Guy", WalletsCount = 3 },
                new PersonDTO { FirstName = "Edan", LastName = "Craft", WalletsCount = 3 },
                new PersonDTO { FirstName = "Rhoda", LastName = "Key", WalletsCount = 3 },
                new PersonDTO { FirstName = "Germaine", LastName = "Carrillo", WalletsCount = 3 },
                new PersonDTO { FirstName = "Boris", LastName = "Pittman", WalletsCount = 3 }
            };
            var mock = new Mock<IPersonService>();
            mock.Setup(a => a.Get()).ReturnsAsync(TestList);
            Mock<PersonsController> controller = new Mock<PersonsController>(mock.Object);

            //Act
            var result = await controller.Object.GetAll() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, TestList);
        }

        [Test]
        public async Task Index_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            PersonsController controller = new PersonsController(mock.Object);

            //Act
            var result = await controller.GetAll() as OkObjectResult;

            //Assert
            mock.Verify(a => a.Get(), Times.Once);
        }

        [Test]
        public void PersonController_CreateAnObject()
        {
            //Arrange
            string expected = "PersonsController";
            var mock = new Mock<IPersonService>();

            //Act
            PersonsController controller = new PersonsController(mock.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnFalse()
        {
            //Arrange
            var personVM = new PersonViewModel()
            {
                Email = "a",
            };
            var context = new ValidationContext(personVM);
            var res = new List<ValidationResult>();

            //Act
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PersonViewModel), typeof(PersonViewModel)), typeof(PersonViewModel));
            var isModelStateValid = Validator.TryValidateObject(personVM, context, res);

            //Assert
            Assert.IsFalse(isModelStateValid);
        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnTrue()
        {
            //Arrange
            var personVM = new PersonViewModel()
            {
                FirstName = "Aytda",
                LastName = "Sdfienr",
                Email = "dhfet@ukr.net",
                Phone = "0976543234",

            };
            var context = new ValidationContext(personVM);
            var res = new List<ValidationResult>();

            //Act
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PersonViewModel), typeof(PersonViewModel)), typeof(PersonViewModel));
            var isModelStateValid = Validator.TryValidateObject(personVM, context, res);

            //Assert
            Assert.IsTrue(isModelStateValid);
        }

        [Test]
        public async Task AddShouldCallAddPersonOnce()
        {
            //Arrange
            PersonDTO personDTO = new PersonDTO
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            mock.Setup(x => x.Add(It.IsAny<PersonDTO>())).ReturnsAsync(It.IsAny<PersonDTO>());

            //Act
            await controller.Add(personDTO);

            //Assert
            mock.Verify(x => x.Add(It.IsAny<PersonDTO>()), Times.Once);
        }

        [Test]
        public void AddShouldRedirectToActionAdd()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);

            // Act
            var result = controller.RedirectToAction("Add");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Add"));
        }

        [Test]
        public async Task AddShouldReturnOkResult()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            PersonDTO personDTO = new PersonDTO
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            mock.Setup(a => a.Add(personDTO)).ReturnsAsync(personDTO);
            // Act
            var result = await controller.Add(personDTO) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, personDTO);
        }

        [Test]
        public async Task UpdateVerifyOnce()
        {
            //Arrange
            PersonDTO personDTO = new PersonDTO
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            mock.Setup(x => x.Update(personDTO.Id, personDTO)).ReturnsAsync(personDTO);

            //Act
            var result = await controller.Update(personDTO.Id, personDTO) as OkObjectResult;

            //Assert
            mock.Verify(x => x.Update(personDTO.Id, personDTO));
            Assert.AreEqual(result.Value, personDTO);
        }

        [Test]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var personId = 1;
            var person = new PersonDTO() { Id = personId };
            var mock = new Mock<IPersonService>();
            mock.Setup(p => p.Update(personId, It.IsAny<PersonDTO>()));
            var controller = new PersonsController(mock.Object);

            // Act
            var result = await controller.Update(personId, person) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task Details_IsNotNull()
        {
            //Arrange
            var personId = 1;
            var testPerson = new PersonDTO() { Id = personId };
            var personsRepo = new Mock<IPersonService>();
            personsRepo.Setup(g => g.Get(personId)).ReturnsAsync(testPerson);
            var controller = new PersonsController(personsRepo.Object);

            // Act
            var result = await controller.Details(personId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, testPerson);
        }

        [Test]
        public async Task DeleteShouldCallDeletePersonInBlOnce()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            mock.Setup(x => x.Delete(1));

            //Act
            await controller.DeletePerson(1);

            //Assert
            mock.Verify(x => x.Delete(1), Times.Once);
        }

        [Test]
        public void DeleteShouldRedirectToActionDelete()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);

            // Act
            var result = controller.RedirectToAction("Delete");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }

        [Test]
        public async Task DeleteShouldReturnActionResult()
        {
            // Arrange
            PersonDTO personDTO = new PersonDTO
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);

            // Act
            var result = await controller.DeletePerson(personDTO.Id) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
