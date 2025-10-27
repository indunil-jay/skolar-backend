using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skolar.Application.Todos.Commands;

namespace Skolar.Api.Controllers.Todos;

[Route("api/todos")]
[ApiController]
public class Todos : ControllerBase
{
    private readonly ISender _sender;
    public Todos(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(
            [FromBody] CreateTodoRequest request,
            CancellationToken cancellationToken)
    {
       
      var todo =  await _sender.Send(new CreateTodoCommand(
            request.Title,
            request.Description,
            request.Priority,
            request.DueDate), cancellationToken);
        return Ok(todo);
    }
}
