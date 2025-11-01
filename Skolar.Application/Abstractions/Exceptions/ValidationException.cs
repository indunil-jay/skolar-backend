using Skolar.Domain.Primitives;

namespace Skolar.Application.Abstractions.Exceptions;


internal sealed class ValidationException : Exception
{
    public IReadOnlyCollection<Error> Errors { get; }

    public ValidationException(IEnumerable<Error> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToList().AsReadOnly();
    }
}