using FamilyAccounting.DAL.Connection;
using FamilyAccounting.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.DAL.Repositories
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(_ => new DbConfig { ConnectionString = connectionString });
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<ITransactionRepository,TransactionRepository>();
            return services;
        }
    }
}
