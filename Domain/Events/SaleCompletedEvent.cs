using DomainDrivenDesign.Domain.Models;
using MediatR;

namespace DomainDrivenDesign.Domain.Events;

public class SaleCompletedEvent : INotification
{
    public Sale Sale { get; }
    public SaleCompletedEvent(Sale sale) => Sale = sale;
}
