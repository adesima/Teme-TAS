namespace BankingApp.Tests;

using Xunit;

public class DepositTest
{
    private Account account;
    
    // --------------------- Unit Tests ---------------------
    [Fact]
    public void Deposit_Pass()
    {
        // act
        account = new Account(100);
        account.Deposit(50);
        
        // assert
        Assert.Equal(150, account.Balance);
    }
    
    [Fact]
    public void Deposit_Fail_NegativeAmount()
    {
        account = new Account(10);
        
        Assert.Throws<NegativeAmountException>(() =>
            account.Deposit(-10f));
    }

    // --------------------- Domain Tests ------------------------
    [Theory]
    [InlineData(0.1f)]
    [InlineData(0)]
    [InlineData(-0.1)]
    public void Deposit_BoundaryValues(float amount)
    {
        float initialBalance = 50;
        account = new Account(initialBalance);

        if (amount > 0)
        {
            account.Deposit(amount);
            Assert.Equal(initialBalance + amount, account.Balance);
        }
        else if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                account.Deposit(amount));
        }
    }
}