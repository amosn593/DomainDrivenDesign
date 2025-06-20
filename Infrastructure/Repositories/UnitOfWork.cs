using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Infrastructure.DataContext;

namespace DomainDrivenDesign.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository ProductRepository { get; }
    public ISaleRepository SaleRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        ProductRepository = new ProductRepository(context);
        SaleRepository = new SaleRepository(context);
        CustomerRepository = new CustomerRepository(context);
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
