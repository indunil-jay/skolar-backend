namespace Skolar.Domain.Primitives;

public abstract class AggregateRoot : BaseEntity
{
    protected AggregateRoot() : base() { } // For EF Core
    protected AggregateRoot(Guid id) : base(id) { }
}
