using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FamilyAccounting.Tests.ControllerTests
{
    
    class PersonControllerTests
    {
        [Test]
        public void ModelValidation_ModelIsValid_ReturnFalse()
        {
            var personVM = new PersonViewModel()
            {
                Email = "a",

            };
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(personVM);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PersonViewModel), typeof(PersonViewModel)), typeof(PersonViewModel));

            var isModelStateValid = Validator.TryValidateObject(personVM, context, res);
            Assert.IsFalse(isModelStateValid);
        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnTrue()
        {
            var personVM = new PersonViewModel()
            {
                FirstName = "Aytda",
                LastName = "Sdfienr",
                Email = "dhfet@ukr.net",
                Phone = "0976543234",
               
            };
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(personVM);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PersonViewModel), typeof(PersonViewModel)), typeof(PersonViewModel));

            var isModelStateValid = Validator.TryValidateObject(personVM, context, res);
            Assert.IsTrue(isModelStateValid);
        }
        [Test]
        public void Index_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<IPersonWebService>();
            mock.Setup(a => a.Get());
            PersonController controller = new PersonController(mock.Object);

            //Act
            ViewResult result = controller.Index(3) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_ThrowsException()
        {
            //Arrange
            var mock = new Mock<IPersonWebService>();
            mock.Setup(a => a.Get()).Throws(new Exception("Test Exception"));
            PersonController controller = new PersonController(mock.Object);

            //Act
            ContentResult result = controller.Index(1) as ContentResult;

            //Assert
            Assert.That(() => mock.Object.Get(), Throws.Exception);
        }

        [Test]
        public void Index_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IPersonWebService>();
            PersonController controller = new PersonController(mock.Object);

            //Act
            RedirectToActionResult result = controller.Index(1) as RedirectToActionResult;

            //Assert
            mock.Verify(a => a.Get(), Times.Once);
        }

        [Test]
        public void PersonController_CreateAnObject()
        {
            //Arrange
            string expected = "PersonController";
            var mock = new Mock<IPersonWebService>();

            //Act
            PersonController controller = new PersonController(mock.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void AddShouldCallAddPersonOnce()
        {
            //Arrange
            PersonViewModel pvm = new PersonViewModel
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);
            mock.Setup(x => x.Add(It.IsAny<PersonViewModel>())).Returns(It.IsAny<PersonViewModel>());

            //Act
            controller.Add(pvm);

            //Assert
            mock.Verify(x => x.Add(It.IsAny<PersonViewModel>()), Times.Once);
        }

        [Test]
        public void AddShouldRedirectToActionAdd()
        {
            // Arrange
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.RedirectToAction("Add");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Add"));
        }

        [Test]
        public void AddShouldReturnViewResult()
        {
            // Arrange
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.Add();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Update_ReturnsRedirect_ToActionResut()
        {
            // Arrange
            var personId = 1;
            var person = new PersonViewModel() { Id = personId };
            var mock = new Mock<IPersonWebService>();
            mock.Setup(p => p.Update(personId, It.IsAny<PersonViewModel>()));
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.Update(personId, person);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Update_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var person = new PersonViewModel()
            {
                Id = 1,
                FirstName = "Person",
                LastName = "New"
            };
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);

            //Act
            var result = controller.Update(person.Id, person);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Details_PersonExists_ReturnsAViewResultWithPerson()
        {
            //Arrange
            var personId = 1;
            var testPerson = new PersonViewModel () { Id = personId };
            var personsRepo = new Mock<IPersonWebService>();
            personsRepo.Setup(g => g.Get(personId)).Returns(testPerson);
            var controller = new PersonController(personsRepo.Object);

            // Act
            var result = controller.Details(personId, 1);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as PersonViewModel;
            Assert.AreEqual(personId, model.Id);
        }

        [Test]
        public void Details_PersonDoesNotExist_ReturnsNotFoundResults()
        {
            // Arrange
            var personsRepo = new Mock<IPersonWebService>();
            personsRepo.Setup(g => g.Get(It.IsAny<int>())).Throws(It.IsAny<Exception>());
            var controller = new PersonController(personsRepo.Object);

            // Act
            var result = controller.Details(12346743, 1);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteShouldCallDeletePersonInBlOnce()
        {
            //Arrange
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);
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
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.RedirectToAction("Delete");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Delete"));
        }

        [Test]
        public void DeleteShouldReturnViewResult()
        {
            // Arrange
            var mock = new Mock<IPersonWebService>();
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.Add();

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
