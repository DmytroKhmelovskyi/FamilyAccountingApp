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
            services.AddTransient<IPersonsRepository, PersonsRepository>();
            services.AddTransient<ICardsRepository, CardsRepository>();
            services.AddTransient<IWalletsRepository, WalletsRepository>();
            return services;
        }

    }
}
