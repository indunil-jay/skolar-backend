using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skolar.Domain.Todos;
using Skolar.Infrastructure.Repositories;

namespace Skolar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString)
                   .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ITodoRepository, TodoRepository>();
        return services;
    }
}
