using Microsoft.AspNetCore.Mvc;
using Skolar.Domain.Primitives;
using Skolar.Domain.Shared;

namespace Skolar.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            
            var statusCode = typeof(T).Name.EndsWith("Response", StringComparison.OrdinalIgnoreCase)
                ? StatusCodes.Status201Created
                : StatusCodes.Status200OK;

            return controller.StatusCode(statusCode, new ApiResponse<T>(result.Value));
        }

        // ❌ Validation errors (400)
        if (result is IValidationResult validationResult)
        {
            return controller.BadRequest(CreateProblemDetails(
                ErrorCodes.Validation,
                StatusCodes.Status400BadRequest,
                result.Error,
                validationResult.Errors
            ));
        }

        // 🚫 Unauthorized (401)
        if (result.Error.Code == ErrorCodes.Unauthorized)
        {
            return controller.Unauthorized(CreateProblemDetails(
                ErrorCodes.Unauthorized,
                StatusCodes.Status401Unauthorized,
                result.Error
            ));
        }

        // 🔍 Not Found (404)
        if (result.Error.Code == ErrorCodes.NotFound)
        {
            return controller.NotFound(CreateProblemDetails(
                ErrorCodes.NotFound,
                StatusCodes.Status404NotFound,
                result.Error
            ));
        }

        // 💥 Default → 500 Internal Server Error
        return controller.StatusCode(
            StatusCodes.Status500InternalServerError,
            CreateProblemDetails(
                ErrorCodes.Internal,
                StatusCodes.Status500InternalServerError,
                result.Error
            ));
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null)
    {
        var problem = new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Type = error.Code,
        };

        if (errors is not null)
            problem.Extensions[nameof(errors)] = errors;

        return problem;
    }
}
