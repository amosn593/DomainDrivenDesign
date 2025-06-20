using DomainDrivenDesign.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Infrastructure.DataContext;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;
    public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options) => _mediator = mediator;
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAudit> ProductAudits => Set<ProductAudit>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<AccountAudit> AccountAudits => Set<AccountAudit>();

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

        modelBuilder.Entity<Product>()
        .HasMany(p => p.AuditTrail)
        .WithOne()
        .HasForeignKey(a => a.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Account>()
        .HasMany(a => a.AuditTrail)
        .WithOne()
        .HasForeignKey(aa => aa.AccountId)
        .OnDelete(DeleteBehavior.Cascade);
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