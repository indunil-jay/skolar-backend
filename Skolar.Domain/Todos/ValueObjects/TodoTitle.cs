using Skolar.Domain.Primitives;

namespace Skolar.Domain.Todos.ValueObjects;

public sealed class TodoTitle : ValueObject
{
    public string Value { get; }

    private TodoTitle(string value)
    {
        Value = value;
    }

    public static TodoTitle Create(string value)
    {
        
        return new TodoTitle(value.Trim());
    }

    public static implicit operator string(TodoTitle title) => title.Value;

    public static explicit operator TodoTitle(string value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value; 
    }
}
