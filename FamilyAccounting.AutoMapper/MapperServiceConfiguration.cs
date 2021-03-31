using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyAccounting.AutoMapper
{
    public static class MapperServiceConfiguration
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
