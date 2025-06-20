using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using DomainDrivenDesign.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context) => _context = context;

    public void Add(Product product) => _context.Products.Add(product);
    public Task<Product?> GetByIdAsync(int id) => _context.Products.FindAsync(id).AsTask();
}

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;
    public SaleRepository(AppDbContext context) => _context = context;
    public void Add(Sale entity) => _context.Sales.Add(entity);
    public Task<Sale?> GetByIdAsync(int id) => _context.Sales.FindAsync(id).AsTask();
}

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;
    public CustomerRepository(AppDbContext context) => _context = context;
    public void Add(Customer entity) => _context.Customers.Add(entity);
    public Task<Customer?> GetByIdAsync(int id) => _context.Customers.FindAsync(id).AsTask();
}

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    public AccountRepository(AppDbContext context) => _context = context;

    public Task<Account?> GetByIdAsync(int id) =>
        _context.Accounts.Include(a => a.AuditTrail).FirstOrDefaultAsync(a => a.Id == id);

    public Task<Account?> GetByNameAsync(string name) =>
        _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);

    public void Add(Account account) => _context.Accounts.Add(account);

    public void Update(Account account) => _context.Accounts.Update(account);
}
