using FamilyAccounting.Api;
using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using FamilyAccounting.Tests.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http;

namespace FamilyAccounting.Tests
{
    public class TestClientProvider

    {
        public HttpClient Client { get; private set; }
        protected Mock<IPersonRepository> PersonRepoMock = new Mock<IPersonRepository>();
        protected Mock<ICardRepository> CardRepoMock = new Mock<ICardRepository>();
        protected Mock<IWalletRepository> WalletRepoMock = new Mock<IWalletRepository>();
        protected Mock<ITransactionRepository> TransactionRepoMock = new Mock<ITransactionRepository>();
        protected Mock<IAuthenticationRepository> AuthenticationRepoMock = new Mock<IAuthenticationRepository>();
        protected Mock<IAuditRepository> AuditRepoMock = new Mock<IAuditRepository>();

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.json");
            }).ConfigureTestServices(services =>
            {
                services.AddTransient<IPersonRepository>(_ => PersonRepoMock.Object);
                services.AddTransient<ICardRepository>(_ => CardRepoMock.Object);
                services.AddTransient<IWalletRepository>(_ => WalletRepoMock.Object);
                services.AddTransient<ITransactionRepository>(_ => TransactionRepoMock.Object);
                services.AddTransient<IAuthenticationRepository>(_ => AuthenticationRepoMock.Object);
                services.AddTransient<IAuditRepository>(_ => AuditRepoMock.Object);
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "Test", options => { });
            })
            .UseStartup<Startup>());
            Client = server.CreateClient();
        }

    }
}

