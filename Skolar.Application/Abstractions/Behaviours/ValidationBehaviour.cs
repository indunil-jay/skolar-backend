using FluentValidation;
using MediatR;
using Skolar.Application.Abstractions.Errors;

namespace Skolar.Application.Abstractions.Behaviours;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationErrors = _validators
                .Select(v => v.Validate(context))
                .Where(result => result.Errors.Any())

                .SelectMany(result => result.Errors)
                .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
                .ToList();

            if (validationErrors.Any())
                throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}
