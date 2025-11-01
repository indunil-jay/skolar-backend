using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skolar.Application.Abstractions.Behaviors;

namespace Skolar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        mapsterConfig.Scan(typeof(DependencyInjection).Assembly);
        mapsterConfig.Compile();
        services.AddSingleton(mapsterConfig);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationPipelineBehavior<,>));    

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));

        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,includeInternalTypes:true);

        return services;
    }
}
