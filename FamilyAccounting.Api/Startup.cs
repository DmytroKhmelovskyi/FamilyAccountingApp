using FamilyAccounting.Api.Interfaces;
using FamilyAccounting.Api.Middlewares;
using FamilyAccounting.Api.Services;
using FamilyAccounting.AutoMapper;
using FamilyAccounting.BL.Services;
using FamilyAccounting.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace FamilyAccounting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins(
                            "https://localhost:44393",
                            "https://localhost:5001").WithHeaders(HeaderNames.ContentType, "x-custom-header")
                                .WithMethods("PUT", "DELETE", "GET");
                    });
            });
            services.AddControllers();
            services.AddServices();
            services.AddModelMapping();
            services.AddTransient<ILoginApiService, LoginApiService>();
            services.AddRepositories(connectionString);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });
            services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Title = "FamilyAccounting",
                   Version = "v1"
               });
           });
            //services.AddCors();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionMiddleware();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "FamilyAccounting");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
