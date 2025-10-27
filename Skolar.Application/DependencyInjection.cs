using FluentValidation;
using Mapster;
using MapsterMapper;
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

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        var mapsterConfig  = TypeAdapterConfig.GlobalSettings;
        mapsterConfig.Scan(typeof(DependencyInjection).Assembly); 
        services.AddSingleton(mapsterConfig);
        services.AddScoped<IMapper, ServiceMapper>();


        return services;
    }
}
