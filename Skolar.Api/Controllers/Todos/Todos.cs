using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skolar.Application.Todos.Commands;

namespace Skolar.Api.Controllers.Todos;

[Route("api/todos")]
[ApiController]
public class Todos : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public Todos(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(
            [FromBody] CreateTodoRequest request,
            CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTodoCommand>(request);
        var todo =  await _sender.Send(command, cancellationToken);
        return Ok(todo);
    }
}
