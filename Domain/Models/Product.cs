using DomainDrivenDesign.Domain.Events;
using DomainDrivenDesign.Domain.ValueObjects;

namespace DomainDrivenDesign.Domain.Models;

public class Product : Entity
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public int StockQuantity { get; private set; }
    private Product() { }
    private Product(string name, Money price, int quantity)
    {
        Name = name;
        Price = price;
        StockQuantity = quantity;
        AddDomainEvent(new ProductCreatedEvent(this));
    }
    public static Product Create(string name, decimal price, int quantity) => new(name, new Money(price), quantity);
    public void DecreaseStock(int quantity)
    {
        if (quantity > StockQuantity) throw new InvalidOperationException("Not enough stock");
        StockQuantity -= quantity;
    }
}
