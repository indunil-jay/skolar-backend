using MediatR;

namespace Skolar.Domain.Primitives;

/// <summary>
/// Represents the base class for all aggregate roots and entities in the domain layer.
/// Provides a unique identifier and domain event management capabilities.
/// </summary>
/// <remarks>
/// In Domain-Driven Design (DDD), the <see cref="BaseEntity"/> is a foundational abstraction that:
/// - Guarantees every entity has a unique <see cref="Id"/>.
/// - Provides built-in support for publishing domain events.
/// - Implements equality and hash code logic based on entity identity.
///
/// This helps enforce consistent behavior across all domain entities and
/// makes it easier to track and dispatch domain events using the <see cref="MediatR"/> library.
/// </remarks>
public abstract class BaseEntity : IEquatable<BaseEntity>
{
    /// <summary>
    /// The private collection of domain events that have occurred within this entity.
    /// </summary>
    /// <remarks>
    /// Domain events capture significant state changes or business actions 
    /// (e.g., "OrderPlaced", "UserRegistered") that other parts of the system may need to react to.
    ///
    /// Using <see cref="INotification"/> (from MediatR) allows publishing events
    /// through the MediatR pipeline, ensuring loose coupling between entities and event handlers.
    /// </remarks>
    private readonly List<INotification> _domainEvents = [];

    /// <summary>
    /// Protected parameterless constructor for ORM tools (like EF Core) and inheritance.
    /// </summary>
    protected BaseEntity() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class with a specific unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity instance.</param>
    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the unique identifier of this entity.
    /// </summary>
    /// <remarks>
    /// The <see cref="Id"/> is immutable and serves as the entity’s identity.
    /// Equality and hash codes are based on this value.
    /// </remarks>
    public Guid Id { get; private set; }

    /// <summary>
    /// Returns an immutable list of domain events that have occurred within this entity.
    /// </summary>
    /// <returns>A read-only collection of <see cref="INotification"/> domain events.</returns>
    public IReadOnlyCollection<INotification> GetDomainEvents() => _domainEvents.AsReadOnly().ToList();

    /// <summary>
    /// Adds a domain event to the entity’s internal event list.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    /// <remarks>
    /// This method is typically called when an important state change occurs within the entity.
    /// For example, after creating a new "Todo" item, you might publish a <c>TodoCreatedEvent</c>.
    /// </remarks>
    public void PublishDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);

    /// <summary>
    /// Clears all domain events after they have been published or handled.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns><c>true</c> if the objects are equal; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Entity equality is based solely on identity (<see cref="Id"/>).
    /// Two entities with the same ID are considered equal, even if other fields differ.
    /// </remarks>
    public override bool Equals(object? obj)
    {
        if (obj is not BaseEntity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    /// <summary>
    /// Generates a hash code based on the entity's type and ID.
    /// </summary>
    /// <returns>A hash code representing the entity.</returns>
    public override int GetHashCode() => HashCode.Combine(GetType(), Id);

    /// <summary>
    /// Determines whether the specified <see cref="BaseEntity"/> is equal to the current entity.
    /// </summary>
    /// <param name="other">The entity to compare to the current instance.</param>
    /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
    public bool Equals(BaseEntity? other) => Equals((object?)other);

    /// <summary>
    /// Equality operator to compare two entities.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if both entities are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(BaseEntity? left, BaseEntity? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator to compare two entities.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if both entities are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(BaseEntity? left, BaseEntity? right) => !(left == right);
}
