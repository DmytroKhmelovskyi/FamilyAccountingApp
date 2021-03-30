using FamilyAccounting.Web.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.Web.Services
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddWebRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPersonWebService, PersonWebService>();
            services.AddTransient<ICardWebService, CardWebService>();
            services.AddTransient<IWalletWebService, WalletWebService>();
            services.AddTransient<ITransactionWebService, TransactionWebService>();
            services.AddTransient<ILoginWebService, LoginWebService>();
            services.AddTransient<IAuditWebService, AuditWebService>();
            return services;
        }

    }
}
