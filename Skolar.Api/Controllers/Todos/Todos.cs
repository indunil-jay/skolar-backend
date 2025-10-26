using Microsoft.AspNetCore.Mvc;

namespace Skolar.Api.Controllers.Todos;

[Route("api/todos")]
[ApiController]
public class Todos : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateTodo(
            [FromBody] CreateTodoRequest request,
            CancellationToken cancellationToken)
    {
       
        return Ok();
    }
}
