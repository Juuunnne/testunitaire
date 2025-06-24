namespace Bank;

public class BankAccount : IBankAccount
{
    public decimal Balance { get; set; }
    public string AccountNumber { get; set; }
    public List<string> TransactionHistory { get; private set; }


    public BankAccount(string accountNumber, decimal initialBalance = decimal.Zero)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be null or empty");

        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative");

        AccountNumber = accountNumber;
        Balance = initialBalance;
        TransactionHistory = new();
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Deposit amount should be positive");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Withdraw amount should be positive");
        Balance -= amount;
    }

    public void Transfer(BankAccount destinationAccount, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(destinationAccount);

        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be positive");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds for transfer");

        if (destinationAccount.AccountNumber == AccountNumber)
        {
            throw new ArgumentNullException("Cannot transfer to the same account");
        }

        Balance -= amount;
        destinationAccount.Balance += amount;
        TransactionHistory.Add($"Transfert: -{amount:C}");
    }
}