using AutoMapper;
using FamilyAccounting.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.Web.Services
{
    public static class MapperServiceConfiguration
    {
        public static IServiceCollection AddViewModelMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
