using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skolar.Domain.Primitives;
using Skolar.Domain.Todos;
using Skolar.Infrastructure.Interceptors;
using Skolar.Infrastructure.Repositories;

namespace Skolar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        // -------------------------
        // Database / EF Core
        // -------------------------
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>((serviceProvider,optionsBuilder) =>
        {
            var interceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            optionsBuilder.UseSqlServer(connectionString)
                   .UseSnakeCaseNamingConvention()
                   .AddInterceptors(interceptor!);
        });
        //Unit Of Work
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());


        // -------------------------
        // Repositories
        // -------------------------
        services.AddScoped<ITodoRepository, TodoRepository>();
        return services;
    }
}
