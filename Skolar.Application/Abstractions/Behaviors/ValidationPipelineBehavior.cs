using FluentValidation;
using MediatR;
using Skolar.Domain.Shared;

namespace Skolar.Application.Abstractions.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();

        }
        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(ValidationResult => ValidationResult.Errors)
            .Where(validationFailure=> validationFailure is not null)
            .Select(f => new Error(f.PropertyName, f.ErrorMessage))
            .Distinct()
            .ToArray();

        if (validationErrors.Any())
        {
            return CreateValidationResult<TResponse>(validationErrors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {

        if(typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.Fail(errors) as TResult)!;
        }

      object validationResult =  typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.Fail))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
