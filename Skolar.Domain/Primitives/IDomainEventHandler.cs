namespace Skolar.Domain.Primitives;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{ 
    Task Handle(TDomainEvent obj, CancellationToken cancellationToken);
}
