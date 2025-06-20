using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Infrastructure.DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Application;

public record StockInCommand(int ProductId, int Quantity) : IRequest<bool>;
public record StockOutCommand(int ProductId, int Quantity) : IRequest<bool>;

public class StockInHandler : IRequestHandler<StockInCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public StockInHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        product.IncreaseStock(request.Quantity);
        var inventory = await _unitOfWork.AccountRepository.GetByNameAsync("Inventory");
        inventory?.Credit(product.Price.Amount * request.Quantity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

public class StockOutHandler : IRequestHandler<StockOutCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public StockOutHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(StockOutCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        product.DecreaseStock(request.Quantity);
        var inventory = await _unitOfWork.AccountRepository.GetByNameAsync("Inventory");
        inventory?.Debit(product.Price.Amount * request.Quantity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
