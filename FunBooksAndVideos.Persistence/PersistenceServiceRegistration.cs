using FunBooksAndVideos.Application.Contracts.Persistence;
using FunBooksAndVideos.Persistence.DatabaseContext;
using FunBooksAndVideos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FunBooksAndVideos.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FunBooksAndVideosDbContext>(options =>        
            options.UseSqlite(configuration.GetConnectionString("SqLiteDefaultConnection"))
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

        return services;
    }
}


