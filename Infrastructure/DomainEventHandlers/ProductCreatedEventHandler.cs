using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using DomainDrivenDesign.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.DomainEventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCreatedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        var product = notification.Product;
        var inventory = await _unitOfWork.AccountRepository.GetByNameAsync("Inventory");
        decimal value = product.Price.Amount * product.StockQuantity;
        inventory?.Credit(value);
        await _unitOfWork.SaveChangesAsync();
    }
}

