namespace Solid._03_LSP.Good;

public abstract class BankAccount
{
    public decimal Balance { get; protected set; }
    public abstract void Withdraw(decimal amount);
}

public class ChekingAccount : BankAccount
{
    public override void Withdraw(decimal amount)
    {
        throw new NotImplementedException();
    }
}

public class SavingsAccount : BankAccount
{
    public override void Withdraw(decimal amount)
    {
        if (Balance < amount)
            throw new Exception("Insufficient balance");

        Balance -= amount;
        Console.WriteLine($"Withdraw {amount}");
    }
}