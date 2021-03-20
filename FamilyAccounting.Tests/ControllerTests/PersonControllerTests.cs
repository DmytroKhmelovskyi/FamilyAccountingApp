using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Controllers;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonControllerTests
    {
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
    }
}
