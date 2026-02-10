using FunBooksAndVideos.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FunBooksAndVideos.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            // Behaviors are registered in the order they are added! SaveChangesBehavior is added before TransactionBehavior
            // to ensure that changes are saved before the transaction is committed.
            cfg.AddOpenBehavior(typeof(SaveChangesBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        return services;
    }
}
