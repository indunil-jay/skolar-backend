namespace Skolar.Domain.Primitives;

public sealed class ValidationResult :Result,IValidationResult 
{

    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult Fail(Error[] errors) => new(errors);
}


public sealed class ValidationResult<T> : Result<T>, IValidationResult
{
    private ValidationResult(Error[] errors) : base(default!, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }
    public Error[] Errors { get; }
    public static ValidationResult<T> Fail(Error[] errors) => new(errors);
}
