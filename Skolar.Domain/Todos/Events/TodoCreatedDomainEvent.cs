using Skolar.Domain.Primitives;
namespace Skolar.Domain.Todos.Events;
public sealed record TodoCreatedDomainEvent(Todo Todo) : IDomainEvent;
