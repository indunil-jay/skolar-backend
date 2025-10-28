using MediatR;

namespace Skolar.Domain.Primitives;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = [];

    protected Entity() { }
    protected Entity(Guid id) { 
     Id = id;
    }
    public Guid Id { get; private set; }
    public IReadOnlyCollection<INotification> GetDomainEvents() => _domainEvents.AsReadOnly().ToList();
    public void PublishDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
       
}
