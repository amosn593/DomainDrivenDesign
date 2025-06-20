namespace DomainDrivenDesign.Domain.Models;

public class Account : Entity
{
    public string Name { get; private set; } = null!;
    public decimal Balance { get; private set; }
    public List<AccountAudit> AuditTrail { get; private set; } = new();

    private Account() { }

    public Account(string name, decimal initialBalance = 0)
    {
        Name = name;
        Balance = initialBalance;
    }

    public void Credit(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Zero of negative funds.");
        Balance += amount;
        AuditTrail.Add(new AccountAudit(Id, amount, "Credit"));
    }

    public void Debit(decimal amount)
    {
        if (Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        AuditTrail.Add(new AccountAudit(Id, -amount, "Debit"));
    }
}

public class AccountAudit : Entity
{
    public int AccountId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal AmountChanged { get; private set; }
    public string Operation { get; private set; } = null!;

    private AccountAudit() { }

    public AccountAudit(int accountId, decimal amountChanged, string operation)
    {
        AccountId = accountId;
        AmountChanged = amountChanged;
        Operation = operation;
        Timestamp = DateTime.UtcNow;
    }
}

