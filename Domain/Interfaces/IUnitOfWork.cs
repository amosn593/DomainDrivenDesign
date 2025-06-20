﻿using DomainDrivenDesign.Domain.Models;

namespace DomainDrivenDesign.Domain.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ISaleRepository SaleRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IAccountRepository AccountRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<T> where T : Entity
{
    void Add(T entity);
    Task<T?> GetByIdAsync(int id);
}

public interface IProductRepository : IRepository<Product> { }
public interface ISaleRepository : IRepository<Sale> { }
public interface ICustomerRepository : IRepository<Customer> { }

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> GetByNameAsync(string name);
    void Update(Account account);
}