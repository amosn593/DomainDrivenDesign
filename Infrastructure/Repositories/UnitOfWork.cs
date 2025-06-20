using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Infrastructure.DataContext;

namespace DomainDrivenDesign.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository ProductRepository { get; }
    public ISaleRepository SaleRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    public IAccountRepository AccountRepository { get; }
    public UnitOfWork(AppDbContext context,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        ISaleRepository saleRepository,
        IAccountRepository accountRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        CustomerRepository = customerRepository;
        SaleRepository = saleRepository;
        AccountRepository = accountRepository;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result;
    }
}
