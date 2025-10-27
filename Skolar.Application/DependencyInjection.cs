using Microsoft.Extensions.DependencyInjection;
using Skolar.Application.Abstractions.Behaviours;

namespace Skolar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });

        return services;
    }
}
