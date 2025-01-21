using App.Repositories.Categories;
using App.Repositories.Interceptors;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extentions;

public static class RepositoryExtentitions
{
    //<FrameworkReference Include = "Microsoft.AspNetCore.App" /> projeye eklenir
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection(ConnectionStringOption.key).Get<ConnectionStringOption>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString!.SqlServer,sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
            }); // migration işleminin ekleneceği yeri belirttik
            options.AddInterceptors(new AuditDbContextInterceptor());
        });    
        
        services.AddScoped<IProductRepository, ProductRepository>();  
        services.AddScoped<ICategoryRepository, CategoryRepository>();  
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));        
        services.AddScoped<IUnitOfWork , UnitOfWork>();
        return services;
    }
}
