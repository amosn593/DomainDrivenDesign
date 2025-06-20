namespace DomainDrivenDesign.Domain.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LoyaltyPoints { get; set; }
}

public class SaleItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}

public class SaleDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public CustomerDto Customer { get; set; }
    public decimal Total { get; set; }
    public List<SaleItemDto> Items { get; set; }
}

