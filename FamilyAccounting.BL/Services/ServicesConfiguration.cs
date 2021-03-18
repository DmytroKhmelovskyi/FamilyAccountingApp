using FamilyAccounting.BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.BL.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonsService, PersonsService>();
            return services;
        }
    }
}
