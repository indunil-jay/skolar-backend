using MediatR;

namespace Skolar.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; init; } 
    protected Entity(Guid id) { 
     Id = id;
    }

    private readonly List<INotification> _domainEvents = [];
    public IReadOnlyCollection<INotification> GetDomainEvents() => _domainEvents.AsReadOnly().ToList();
    public void PublishDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
       
}
