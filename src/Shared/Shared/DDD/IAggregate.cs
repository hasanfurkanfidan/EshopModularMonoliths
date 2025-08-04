
namespace Shared.DDD;

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}
public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get;}
    public IDomainEvent[] ClearDomainEvents();
}

