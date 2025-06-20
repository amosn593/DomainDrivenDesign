using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using MediatR;

namespace DomainDrivenDesign.Application;

public record CreateProductCommand(string Name, decimal Price, int Quantity) : IRequest<int>;
public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.Price, request.Quantity);
        _unitOfWork.ProductRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}