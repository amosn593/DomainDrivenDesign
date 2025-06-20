using DomainDrivenDesign.Application;
using DomainDrivenDesign.Domain.Interfaces;
using DomainDrivenDesign.Infrastructure.DataContext;
using DomainDrivenDesign.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure;

public static class DIExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));
        return services;
    }
}
