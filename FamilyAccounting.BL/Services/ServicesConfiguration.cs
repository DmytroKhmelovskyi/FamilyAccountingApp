using FamilyAccounting.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.BL.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ITransactionService,TransactionService>();
            return services;
        }
    }
}
