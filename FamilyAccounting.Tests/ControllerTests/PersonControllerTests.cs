using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonControllerTests
    {
        [Test]
        public void Index_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<IPersonService>();

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
            var mock = new Mock<IPersonService>();
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
            // arrange
            var mock = new Mock<IPersonService>();
            PersonController controller = new PersonController(mock.Object);
            // act
            RedirectToActionResult result = controller.Index(1) as RedirectToActionResult;
            // assert
            mock.Verify(a => a.Get(), Times.Once);
        }

        [Test]
        public void PersonController_CreateAnObject()
        {
            // arrange
            string expected = "PersonController";
            var mock = new Mock<IPersonService>();
            // act
            PersonController controller = new PersonController(mock.Object);
            //assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void AddShouldCallAddPersonInBlOnce()
        {
            //Arrange
            PersonViewModel pvm = new PersonViewModel
            {
                FirstName = "Bob",
                LastName = "Smith",
                Phone = "0636363636",
                Email = "email.email.com"
            };
            var mock = new Mock<IPersonService>();
            var controller = new PersonController(mock.Object);
            mock.Setup(x => x.Add(It.IsAny<PersonDTO>())).Returns(It.IsAny<PersonDTO>());

            //Act
            controller.Add(pvm);

            //Assert
            mock.Verify(x => x.Add(It.IsAny<PersonDTO>()), Times.Once);
        }

        [Test]
        public void AddShouldRedirectToAddView()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
            var controller = new PersonController(mock.Object);

            // Act
            var result = controller.RedirectToAction("Add");

            // Assert
            Assert.That(result.ActionName, Is.EqualTo("Add"));
        }

        [Test]
        public void AddShouldReturnAddView()
        {
            // Arrange
            var mock = new Mock<IPersonService>();
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
            var mock = new Mock<IPersonService>();
            mock.Setup(p => p.Update(personId, It.IsAny<PersonDTO>()));
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
            var guest = new PersonViewModel()
            {
                Id = 1,
                FirstName = "Person",
                LastName = "New"
            };
            var mock = new Mock<IPersonService>();
            var controller = new PersonController(mock.Object);

            //Act
            var result = controller.Update(guest.Id, guest);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
