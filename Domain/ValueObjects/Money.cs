using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Domain.ValueObjects;

[Owned]
public class Money
{
    public decimal Amount { get; private set; }
    private Money() { }
    public Money(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
        Amount = amount;
    }
    public static Money operator *(Money money, int multiplier) => new(money.Amount * multiplier);
    public static implicit operator decimal(Money money) => money.Amount;
}