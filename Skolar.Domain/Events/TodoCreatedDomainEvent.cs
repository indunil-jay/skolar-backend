namespace Skolar.Domain.Events;
using MediatR;

public sealed record TodoCreatedDomainEvent(Todo Todo)   : INotification;
