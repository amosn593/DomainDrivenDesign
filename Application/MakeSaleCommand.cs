using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using MediatR;

namespace DomainDrivenDesign.Application;

public record MakeSaleCommand(int CustomerId, List<AddSaleItemDto> Items) : IRequest<int>;
public record AddSaleItemDto(int ProductId, int Quantity);

public class MakeSaleHandler : IRequestHandler<MakeSaleCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public MakeSaleHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public async Task<int> Handle(MakeSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = Sale.Create(request.CustomerId);
        foreach (var item in request.Items)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            product.DecreaseStock(item.Quantity);
            sale.AddItem(product, item.Quantity);
        }
        var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
        customer.AddLoyaltyPoints((int)sale.Total / 10);
        //sale.AddDomainEvent(new SaleCompletedEvent(sale));
        _unitOfWork.SaleRepository.Add(sale);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return sale.Id;
    }
}

