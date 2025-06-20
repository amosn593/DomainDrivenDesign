using DomainDrivenDesign.Application;
using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Infrastructure.DataContext;
using DomainDrivenDesign.Infrastructure.DomainEventHandlers;
using DomainDrivenDesign.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaleCompletedEventHandler).Assembly));
        return services;
    }
}
