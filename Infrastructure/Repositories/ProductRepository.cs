using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Domain.Models;
using DomainDrivenDesign.Infrastructure.DataContext;

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