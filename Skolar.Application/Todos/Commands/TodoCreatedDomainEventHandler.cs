namespace Skolar.Application.Todos.Commands;
using MediatR;
using Skolar.Domain.Events;

internal  sealed class TodoCreatedDomainEventHandler : INotificationHandler<TodoCreatedDomainEvent>
{
    public TodoCreatedDomainEventHandler()
    {
        //service register here
    }

    public Task Handle(TodoCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //cross cutting concerns here
        return Task.CompletedTask;
    }

}

