using DomainDrivenDesign.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.DataContext;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options) => _mediator = mediator;
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().OwnsOne(p => p.Price);
        modelBuilder.Entity<SaleItem>().OwnsOne(i => i.UnitPrice);

        modelBuilder.Entity<Sale>()
        .HasMany(s => s.Items)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var e in domainEvents)
            await _mediator.Publish(e, cancellationToken);

        foreach (var entry in ChangeTracker.Entries<Entity>())
            entry.Entity.ClearDomainEvents();

        return result;
    }
}