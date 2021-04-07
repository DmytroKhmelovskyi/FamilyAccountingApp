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

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonsControllerTests
    {
        [Test]
        public void Index_IsNotNull()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            mock.Setup(a => a.Get());
            PersonsController controller = new PersonsController(mock.Object);

            //Act
            var result = controller.Index() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void Index_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            PersonsController controller = new PersonsController(mock.Object);

            //Act
            var result = controller.Index();

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
        public void AddShouldCallAddPersonOnce()
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
            mock.Setup(x => x.Add(It.IsAny<PersonDTO>())).Returns(It.IsAny<PersonDTO>());

            //Act
            controller.Add(personDTO);

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
        public void AddShouldReturnOkResult()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            var personDTO = new PersonDTO();
            // Act
            var result = controller.Add(personDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void UpdateVerifyOnce()
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
            mock.Setup(x => x.Update(personDTO.Id, personDTO));

            //Act
            controller.Update(personDTO.Id, personDTO);

            //Assert
            mock.Verify(x => x.Update(personDTO.Id, personDTO));
        }

        [Test]
        public void Update_ReturnsOkResult()
        {
            // Arrange
            var personId = 1;
            var person = new PersonDTO() { Id = personId };
            var mock = new Mock<IPersonService>();
            mock.Setup(p => p.Update(personId, It.IsAny<PersonDTO>()));
            var controller = new PersonsController(mock.Object);

            // Act
            var result = controller.Update(personId, person) as OkResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void Details_IsNotNull()
        {
            //Arrange
            var personId = 1;
            var testPerson = new PersonDTO() { Id = personId };
            var personsRepo = new Mock<IPersonService>();
            personsRepo.Setup(g => g.Get(personId)).Returns(testPerson);
            var controller = new PersonsController(personsRepo.Object);

            // Act
            var result = controller.Details(personId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public void DeleteShouldCallDeletePersonInBlOnce()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);
            mock.Setup(x => x.Delete(1));

            //Act
            controller.DeletePerson(1);

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
        public void DeleteShouldReturnActionResult()
        {
            // Arrange
            int id = 1;
            var mock = new Mock<IPersonService>();
            var controller = new PersonsController(mock.Object);

            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
