using App.Service.Categories;
using App.Service.ExceptionHandlers;
using App.Service.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Service.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();    

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>(); 
            return services;
        }
    }
}
