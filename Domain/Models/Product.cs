using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.ValueObjects;

namespace DomainDrivenDesign.Domain.Models;

public class Product : Entity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public int StockQuantity { get; private set; }
    public List<ProductAudit> AuditTrail { get; private set; } = new();
    private Product() { }
    private Product(string name, Money price, int quantity)
    {
        Name = name;
        Price = price;
        StockQuantity = quantity;
        AddDomainEvent(new ProductCreatedEvent(this));
    }
    public static Product Create(string name, decimal price, int quantity) => new(name, new Money(price), quantity);
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
        StockQuantity += quantity;
        AuditTrail.Add(new ProductAudit(Id, quantity, "StockIn"));
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity > StockQuantity) throw new InvalidOperationException("Not enough stock");
        StockQuantity -= quantity;
        AuditTrail.Add(new ProductAudit(Id, -quantity, "StockOut"));
    }
}


public class ProductAudit : Entity
{
    public int ProductId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public int QuantityChanged { get; private set; }
    public string Operation { get; private set; }

    private ProductAudit() { }

    public ProductAudit(int productId, int quantityChanged, string operation)
    {
        ProductId = productId;
        QuantityChanged = quantityChanged;
        Operation = operation;
        Timestamp = DateTime.UtcNow;
    }
}
