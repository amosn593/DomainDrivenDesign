using MediatR;

namespace DomainDrivenDesign.Domain.Models;

public abstract class Entity
{
    public int Id { get; set; }
    private List<INotification> _domainEvents  = [];
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents;
    protected void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
