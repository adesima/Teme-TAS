namespace BankingApp.Tests;

using Xunit;

public class WithdrawTest
{
    private Account account;
    private float minBalance = Account.MinBalance;
    
    // // ------------------------ Unit Tests -----------------
    [Fact]
    public void Withdraw_Pass()
    {
        account = new Account(100);
        account.Withdraw(50);
        Assert.Equal(50, account.Balance);
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(100)]
    [InlineData(150)]
    public void Withdraw_Fail_ThrowsException(float amount)
    {
        account = new Account(100);
        
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                account.Withdraw(amount));
        }
        else if (account.Balance - amount > 0 && account.Balance - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                account.Withdraw(amount));
        }
        else  if (amount > account.Balance)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                account.Withdraw(amount));
        }
    }
    
    // ---------------------------- Domain Tests ---------------------------
    [Theory]
    [InlineData(0.1f)]
    [InlineData(0)]
    [InlineData(-0.1)]
    [InlineData(0.9)]
    [InlineData(1)]
    [InlineData(1.1)]
    [InlineData(-10)]
    [InlineData(11)]
    [InlineData(11.1)]
    public void Withdraw_BoundaryValues(float amount)
    {
        account = new Account(11);
        
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                account.Withdraw(amount));
        }
        else if (account.Balance - amount > 0 && account.Balance - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                account.Withdraw(amount));
        }
        else  if (amount > account.Balance)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                account.Withdraw(amount));
        }
    }
}