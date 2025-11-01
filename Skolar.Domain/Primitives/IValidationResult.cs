namespace Skolar.Domain.Primitives;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("VALIDATION_ERROR", "A Validation problem occurred.");

    Error[]  Errors { get; } 
}
