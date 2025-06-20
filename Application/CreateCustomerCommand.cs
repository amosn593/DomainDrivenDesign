using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using MediatR;

namespace DomainDrivenDesign.Application;

public record CreateCustomerCommand(string Name) : IRequest<int>;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateCustomerHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Name);
        _unitOfWork.CustomerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id;
    }
}
