using Microsoft.Extensions.Logging;
using Skolar.Domain.Primitives;
using Skolar.Domain.Todos.Events;

namespace Skolar.Application.Todos.Commands;

internal sealed class TodoCreatedDomainEventHandler : IDomainEventHandler<TodoCreatedDomainEvent>
{
    private readonly ILogger<TodoCreatedDomainEventHandler> _logger;    
    public TodoCreatedDomainEventHandler(ILogger<TodoCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoCreatedDomainEvent obj, CancellationToken cancellationToken)
    {
        //cross cutting concerns here
        //ex; logging
        _logger.LogInformation($"Todo Created: ID : {obj.Todo.Id}, NAME : {obj.Todo.Title}");

        return Task.CompletedTask;
    }

}

