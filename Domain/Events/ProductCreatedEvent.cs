using DomainDrivenDesign.Domain.Models;
using MediatR;

namespace DomainDrivenDesign.Domain.Events;

public class ProductCreatedEvent : INotification
{
    public Product Product { get; }
    public ProductCreatedEvent(Product product) => Product = product;
}
