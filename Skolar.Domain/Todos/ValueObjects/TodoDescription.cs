namespace Skolar.Domain.Todos.ValueObjects;

public sealed record TodoDescription
{
    public string? Value { get; }

    public TodoDescription(string? value)
    {
        Value = value;
    }

    public static implicit operator string?(TodoDescription description) => description?.Value;

    public static explicit operator TodoDescription(string? value) => new(value);
}
