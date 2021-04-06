using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class PersonsControllerTests
    {
        [Test]
        public void Index_ViewResultNotNull()
        {
            //Arrange
            var mock = new Mock<IPersonService>();
            mock.Setup(a => a.Get());
            PersonsController controller = new PersonsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsNotNull(result);
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
    }
}
