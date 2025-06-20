using DomainDrivenDesign.Domain.Events;
using MediatR;

namespace DomainDrivenDesign.Infrastructure.DomainEventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Product Created: {notification.Product.Name}");
        return Task.CompletedTask;
    }
}
