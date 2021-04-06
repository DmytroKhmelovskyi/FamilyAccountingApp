using FamilyAccounting.Api.Controllers;
using FamilyAccounting.BL.Interfaces;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.Tests.ControllerTests
{
    class TransactionsControllerTests
    {
        [Test]
        public void TransactionDetails_StatusCodeShouldBeOk()
        {
            //Arrange
            var client = new RestClient("https://localhost:44398/api/Transactions/Details/85?walletId=12&transactionId=85");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
        }  
    }
}

