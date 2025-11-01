using Microsoft.AspNetCore.Mvc;
using Skolar.Domain.Primitives;
using Skolar.Domain.Shared;

namespace Skolar.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        // 1️⃣ Handle success
        if (result.IsSuccess)
        {
            var (statusCode, type) = InferResponseType(controller);

            return controller.StatusCode(
                statusCode,
                ApiResponse<T>.Success(
                    result.Value,
                    type,
                    statusCode
                )
            );
        }

        // 2️⃣ Handle validation error
        if (result is IValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(e => new { e.Code, e.Message });

            return controller.StatusCode(
                StatusCodes.Status400BadRequest,
                ApiResponse<T>.Failure(
                    "VALIDATION_ERROR",
                    StatusCodes.Status400BadRequest,
                    "A validation problem occurred.",
                    errors
                )
            );
        }

        // 3️⃣ Handle Unauthorized
        if (result.Error.Code == ErrorCodes.Unauthorized)
        {
            return controller.StatusCode(
                StatusCodes.Status401Unauthorized,
                ApiResponse<T>.Failure(
                    "UNAUTHORIZED",
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized access.",
                    new[] { new { Code = result.Error.Code, Message = result.Error.Message } }
                )
            );
        }

        // 4️⃣ Handle NotFound
        if (result.Error.Code == ErrorCodes.NotFound)
        {
            return controller.StatusCode(
                StatusCodes.Status404NotFound,
                ApiResponse<T>.Failure(
                    "NOT_FOUND",
                    StatusCodes.Status404NotFound,
                    "Resource not found.",
                    new[] { new { Code = result.Error.Code, Message = result.Error.Message } }
                )
            );
        }

        // 5️⃣ Default: Internal Error
        return controller.StatusCode(
            StatusCodes.Status500InternalServerError,
            ApiResponse<T>.Failure(
                "INTERNAL_ERROR",
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                new[] { new { Code = result.Error.Code, Message = result.Error.Message } }
            )
        );
    }

    /// <summary>
    /// Infers the correct status code and type ("Created", "Updated", etc.) based on the current action.
    /// </summary>
    private static (int StatusCode, string Type) InferResponseType(ControllerBase controller)
    {
        // Use the current executing action name
        var actionName = controller.ControllerContext.ActionDescriptor.ActionName;

        if (actionName.Contains("Create", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status201Created, "Created");

        if (actionName.Contains("Add", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status201Created, "Created");

        if (actionName.Contains("Update", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status200OK, "Updated");

        if (actionName.Contains("Edit", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status200OK, "Updated");

        if (actionName.Contains("Delete", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status200OK, "Deleted");

        if (actionName.Contains("Remove", StringComparison.OrdinalIgnoreCase))
            return (StatusCodes.Status200OK, "Deleted");

        // Default fallback
        return (StatusCodes.Status200OK, "Success");
    }
}
