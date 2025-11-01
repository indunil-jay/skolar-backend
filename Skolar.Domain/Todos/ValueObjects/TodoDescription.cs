using Skolar.Domain.Primitives;

namespace Skolar.Domain.Todos.ValueObjects;

public sealed class TodoDescription : ValueObject
{
    public string? Value { get; }

    private TodoDescription(string? value)
    {
        Value = value;
    }

    public static TodoDescription Create(string? value)
    {
   
        return new TodoDescription(value);
    }

    public static implicit operator string?(TodoDescription description) => description.Value;

    public static explicit operator TodoDescription(string value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
