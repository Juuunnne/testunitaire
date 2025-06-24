using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Bank.UnitTest;

public class BankAccountTest
{
    [Trait("Category", "Account")]
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidAccountNumber_ThrowsArgumentException(string accountNumber)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber));
    }

    [Fact]
    public void Constructor_ValidParams_CreatesAccount()
    {
        // Arrange
        string accountNumber = "12345";
        decimal initialBalance = 10000;

        // Act
        var account = new BankAccount(accountNumber, initialBalance);

        // Assert
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(initialBalance, account.Balance);
        Assert.Empty(account.TransactionHistory);
    }

    [Trait("Category", "Account")]
    [Fact]
    public void Constructor_ValidAccountNumber_SetsProperties()
    {
        // Arrange
        string accountNumber = "12345";
        decimal initialBalance = 100;

        // Act
        var account = new BankAccount(accountNumber, initialBalance);

        // Assert
        Assert.Equal(accountNumber, account.AccountNumber);
        Assert.Equal(initialBalance, account.Balance);
        Assert.NotNull(account.TransactionHistory);
    }

    [Trait("Category", "Account")]
    [Fact]
    public void Constructor_NegativeInitialBalance_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BankAccount("12345", -100));
    }

   
    [Trait("Category", "Transfer")]
    [Fact]
    public void Deposit_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("12345", 100);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => account.Deposit(-50));

    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Deposit_ValidAmount_IncreasesBalance()
    {
        // Arrange
        var account = new BankAccount("12345", 100);
        decimal depositAmount = 50;
        decimal expectedBalance = 150;

        // Act
        account.Deposit(depositAmount);

        // Assert
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Desposit_ZeroAmount_DoesNotChangeBalance()
    {
        // Arrange
        var account = new BankAccount("12345", 100);
        decimal depositAmount = 0;
        decimal expectedBalance = 100;

        // Act
        account.Deposit(depositAmount);

        // Assert
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Withdraw_ValidAmount_DecreasesBalance()
    {
        // Arrange
        var account = new BankAccount("12345", 100);
        decimal withdrawAmount = 50;
        decimal expectedBalance = 50;

        // Act
        account.Withdraw(withdrawAmount);

        // Assert
        Assert.Equal(expectedBalance, account.Balance);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Withdraw_ZeroAmount_DoesNotChangeBalance()
    {
        // Arrange
        var account = new BankAccount("12345", 100);
        decimal withdrawAmount = 0;
        decimal expectedBalance = 100;

        // Act
        account.Withdraw(withdrawAmount);

        // Assert
        Assert.Equal(expectedBalance, account.Balance);

    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Withdraw_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("12345", 100);

        // Act & Assert 
        Assert.Throws<ArgumentException>(() => account.Withdraw(-50));
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Transfer_ValidAmount_UpdatesBalances()
    {
        // Arrange
        var sourceAccount = new BankAccount("12345", 100);
        var destinationAccount = new BankAccount("67890", 50);
        decimal transferAmount = 30;
        decimal expectedSourceBalance = 70;
        decimal expectedDestinationBalance = 80;

        // Act
        sourceAccount.Transfer(destinationAccount, transferAmount);

        // Assert
        Assert.Equal(expectedSourceBalance, sourceAccount.Balance);
        Assert.Equal(expectedDestinationBalance, destinationAccount.Balance);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Transfer_InsufficientFunds_ThrowsInvalidOperationException()
    {
        // Arrange
        var sourceAccount = new BankAccount("12345", 100);
        var destinationAccount = new BankAccount("67890", 50);
        decimal transferAmount = 150;

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => sourceAccount.Transfer(destinationAccount, transferAmount));
        Assert.Equal("Insufficient funds for transfer", exception.Message);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Transfer_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var sourceAccount = new BankAccount("12345", 100);
        var destinationAccount = new BankAccount("67890", 50);
        decimal transferAmount = 0;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => sourceAccount.Transfer(destinationAccount, transferAmount));
        Assert.Equal("Transfer amount must be positive", exception.Message);
    }

    [Trait("Category", "Transfer")]
    [Fact]
    public void Transfer_SameAccount_ThrowsArgumentNullException()
    {
        // Arrange
        var account = new BankAccount("12345", 100);
        decimal transferAmount = 50;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => account.Transfer(account, transferAmount));
    }
}