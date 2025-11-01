namespace Skolar.Domain.Shared;
public enum ErrorType
{
    Failure,
    Validation,
    Problem,
    NotFound,
    Conflict
}

public class Error : IEquatable<Error>
{
    public static readonly Error None = new("NONE", "No error");
    public static readonly Error NullValue = new("NULL_VALUE", "The Specified Result Value is null.");
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }
    public string Message { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not Error other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Code == other.Code;
    }
    public bool Equals(Error? other) => Equals((object?)other);

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? left, Error? right)
    {
        if (left is null && right is null)
            return true;
        if (left is null || right is null)
            return false;
        return left.Code == right.Code && left.Message == right.Message;
    }

    public static bool operator !=(Error? left, Error? right) => !(left == right);
}

