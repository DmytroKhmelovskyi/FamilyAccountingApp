using FamilyAccounting.Web.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Services
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment env;

        public GlobalExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleError(httpContext, ex, env);
            }           
        }

        private Task HandleError(HttpContext httpContext, Exception ex, IWebHostEnvironment env)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            string result = "";

            switch (ex)
            {
                case BadRequestException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            if (env.IsDevelopment())
            {
                result = JsonSerializer.Serialize(new { message = ex?.Message + " STACK TRACE " + ex?.StackTrace });
            }
            else
            {
                result = JsonSerializer.Serialize(new { message = "Oops... Internal Server Error occured!Don't worry, it's being fixed"});
            }
            return response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
