using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FamilyAccounting.Tests.ControllerTests
{
    public class AuditsControllerTests
    {
        [Test]
        public void AuditsController_CreateAnObject()
        {
            //Arrange
            string expected = "AuditsController";
            var mock = new Mock<IAuditService>();

            //Act
            AuditsController controller = new AuditsController(mock.Object);

            //Assert
            Assert.IsNotNull(controller);
            Assert.AreEqual(expected, controller.GetType().Name);
        }

        [Test]
        public void GetPersonsAsync_IsNotNull()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            mock.Setup(a => a.GetPersonsAsync());
            Mock<AuditsController> controller = new Mock<AuditsController>(mock.Object);

            //Act
            var result = controller.Object.GetActions();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetPersonsAsync_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            AuditsController controller = new AuditsController(mock.Object);

            //Act
            var result = controller.GetPersons();

            //Assert
            mock.Verify(a => a.GetPersonsAsync(), Times.Once);
        }
        [Test]
        public void GetActionsAsync_IsNotNull()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            mock.Setup(a => a.GetActionsAsync());
            Mock<AuditsController> controller = new Mock<AuditsController>(mock.Object);

            //Act
            var result = controller.Object.GetActions();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetActionsAsync_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            AuditsController controller = new AuditsController(mock.Object);

            //Act
            var result = controller.GetActions();

            //Assert
            mock.Verify(a => a.GetActionsAsync(), Times.Once);
        }

        [Test]
        public void GetWalletsAsync_IsNotNull()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            mock.Setup(a => a.GetWalletsAsync());
            Mock<AuditsController> controller = new Mock<AuditsController>(mock.Object);

            //Act
            var result = controller.Object.GetWallets();

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetWalletsAsync_VerifyOnce()
        {
            //Arrange
            var mock = new Mock<IAuditService>();
            AuditsController controller = new AuditsController(mock.Object);

            //Act
            var result = controller.GetWallets();

            //Assert
            mock.Verify(a => a.GetWalletsAsync(), Times.Once);
        }
    }
}
