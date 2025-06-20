namespace DomainDrivenDesign.Domain.Models;

public class Customer : Entity
{
    public string Name { get; private set; }
    public int LoyaltyPoints { get; private set; }
    private Customer() { }
    private Customer(string name)
    {
        Name = name;
        LoyaltyPoints = 0;
    }
    public static Customer Create(string name) => new(name);
    public void AddLoyaltyPoints(int points) => LoyaltyPoints += points;
}
