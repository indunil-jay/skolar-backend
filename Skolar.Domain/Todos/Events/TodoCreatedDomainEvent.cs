namespace Skolar.Domain.Todos.Events;
using MediatR;
using Skolar.Domain.Todos;

public sealed record TodoCreatedDomainEvent(Todo Todo)   : INotification;
