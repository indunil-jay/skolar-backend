namespace Skolar.Domain.Todos.ValueObjects;

public sealed record TodoTitle
{
    public string Value { get; }

    public TodoTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("Todo title must be at least 3 characters.");
        Value = value;
    }

    public static implicit operator string(TodoTitle title) => title.Value;
    public static explicit operator TodoTitle(string value) => new(value);
}
