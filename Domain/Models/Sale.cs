using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.ValueObjects;

namespace DomainDrivenDesign.Domain.Models;

public class Sale : Entity
{
    public Customer Customer { get; private set; }
    public int CustomerId { get; private set; }
    public DateTime Date { get; private set; }
    public List<SaleItem> Items { get; private set; } = new();
    public decimal Total => Items.Sum(i => (decimal)i.Total);
    private Sale() { }
    private Sale(int customerId)
    {
        CustomerId = customerId;
        Date = DateTime.UtcNow;
        AddDomainEvent(new SaleCompletedEvent(this));
    }
    public static Sale Create(int customerId) => new(customerId);
    public void AddItem(Product product, int quantity) => Items.Add(SaleItem.Create(product.Id, quantity, product.Price));
}

public class SaleItem : Entity
{
    public Product Product { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money Total => new(UnitPrice.Amount * Quantity);
    private SaleItem() { }
    private SaleItem(int productId, int quantity, Money unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public static SaleItem Create(int productId, int quantity, Money unitPrice) => new(productId, quantity, unitPrice);
}
