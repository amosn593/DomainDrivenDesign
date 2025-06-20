using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.Interfaces;
using MediatR;

namespace DomainDrivenDesign.Infrastructure.DomainEventHandlers;

public class SaleCompletedEventHandler : INotificationHandler<SaleCompletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaleCompletedEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SaleCompletedEvent notification, CancellationToken cancellationToken)
    {
        var sale = notification.Sale;

        var salesAccount = await _unitOfWork.AccountRepository.GetByNameAsync("Sales");
        salesAccount?.Credit(sale.Total);

        var inventoryAccount = await _unitOfWork.AccountRepository.GetByNameAsync("Inventory");
        var costOfGoodsSoldAccount = sale.Items.Sum(x => x.UnitPrice.Amount * x.Quantity);
        inventoryAccount?.Debit(costOfGoodsSoldAccount);
        
        await _unitOfWork.SaveChangesAsync();

        Console.WriteLine($"Sale Completed: {sale.Id}, Total: {sale.Total}");

        await Task.CompletedTask;
    }
}
