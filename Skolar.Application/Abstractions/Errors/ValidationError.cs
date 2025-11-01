namespace Skolar.Application.Abstractions.Errors;

internal record ValidationError(string Field, string Message);
