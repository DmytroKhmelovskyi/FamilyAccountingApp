using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
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
    }
}
