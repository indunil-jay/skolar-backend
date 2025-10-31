using MediatR;
using Microsoft.EntityFrameworkCore;
using Skolar.Domain.Primitives;


namespace Skolar.Infrastructure;

internal sealed class ApplicationDbContext:DbContext,IUnitOfWork
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync(cancellationToken);
            return  result;
        }
        catch (DbUpdateConcurrencyException) {
         throw new Exception("A concurrency conflict occurred. The data you attempted to save has been modified by another user. Please reload the entity and try again.");
        }
    }

    private async Task PublishDomainEventsAsync(CancellationToken cancellationToken) { 
    
        var domainEntities = ChangeTracker
            .Entries<BaseEntity>()
            .Select(entity => entity.Entity)
            .SelectMany(entity=>
            {
                var events = entity.GetDomainEvents();  

                entity.ClearDomainEvents();
                return events;
            })
            .ToList();

        foreach (var domainEvent in domainEntities)
        {
          await _publisher.Publish(domainEvent, cancellationToken);

        }
    }
}
