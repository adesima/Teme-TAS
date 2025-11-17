namespace BankingApp.Tests;
using System;
using Xunit;

public class AccountTest
{
    private Account account;
    private float minBalance = Account.MinBalance;

    // --------------------------------------- Unit Tests ---------------------------------------
    [Fact]
    public void InitializeAccount_Pass()
    {
        // act
        account = new Account(10);
        
        // assert
        Assert.Equal(10, account.Balance);
    }
    
    [Theory]
    [InlineData(5)]
    [InlineData(-1)]
    public void InitializeAccount_Fail_ThrowsExceptions(float balance)
    {
        // act & assert
        if (balance == 5)
            Assert.Throws<MinimumBalanceException>(() =>
                new Account(balance)
            );
        else if (balance == -10)
        {
            Assert.Throws<NegativeAmountException>(() =>
                new Account(balance));
        }
    }

    // ---------------------------------------- Domain Tests -----------------------------------
    [Theory]
    [InlineData(10.1f)]
    [InlineData(10)]
    [InlineData(9.9f)]
    [InlineData(0.1f)]
    [InlineData(0f)]
    [InlineData(-0.1f)]
    public void InitializeAccount_BoundaryValues(float balance)
    {
        if (balance >= minBalance)
        {
            account = new Account(balance);
            Assert.Equal(balance, account.Balance);
        }
        else if (balance >= 0 && balance < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                new Account(balance));
        }
        else if (balance < minBalance)
        {
            Assert.Throws<NegativeAmountException>(() =>
                new Account(balance));
        }
    }
}