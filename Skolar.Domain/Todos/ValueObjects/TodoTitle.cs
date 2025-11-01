using Skolar.Domain.Primitives;
using Skolar.Domain.Shared;

namespace Skolar.Domain.Todos.ValueObjects;

public sealed class TodoTitle : ValueObject
{
    private const int MaxLength = 64;
    private const int MinLength = 3;
    public string Value { get; }

    private TodoTitle(string value)
    {
        Value = value;
    }

    public static Result<TodoTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<TodoTitle>(TodoErrors.TitleIsRequired);

        if (value.Length < MinLength)
            return Result.Failure<TodoTitle>(TodoErrors.TitleTooShort);

        if (value.Length > MaxLength)
            return Result.Failure<TodoTitle>(TodoErrors.TitleTooLong);

        return new TodoTitle(value.Trim());
    }


    public static implicit operator string(TodoTitle title) => title.Value;

    public static explicit operator TodoTitle(string value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value; 
    }
}
