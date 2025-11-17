namespace BankingApp.Tests;

using Xunit;

public class TransferFundsTest
{
    private Account account;
    private float minBalance = Account.MinBalance;
    
    // ------------------------------------------ Unit Tests -------------------------------------
    [Fact]
    public void TransferFunds_Pass()
    {
        var source = new Account(100);
        var destination = new Account(10);
        
        source.TransferFunds(destination, 50);
        
        Assert.Equal(50, source.Balance);
        Assert.Equal(60, destination.Balance);
    }
    
    [Theory]
    [InlineData(100, 50, -1)]
    [InlineData(100, 50, 95)]
    [InlineData(100, 50, 150)]
    public void TrasnferFunds_Fail_ThrowsException(float initialSource, float initialDestination, float amount)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);
        
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFunds(destination, amount));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFunds(destination, amount));
        }
        else if (initialSource < amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFunds(destination, amount));
        }
    }
    
    // ----------------------------------------- Domain Test -------------------------------------------
    [Theory]
    [InlineData(100, 50, 0.1)]
    [InlineData(100, 50, 0)]
    [InlineData(100, 50, -0.1)]
    [InlineData(100, 50, 89.9)]
    [InlineData(100, 50, 90)]
    [InlineData(100, 50, 90.1)]
    [InlineData(100, 50, 99.9)]
    [InlineData(100, 50, 100)]
    [InlineData(100, 50, 100.1)]
    public void TrasnferFunds_BoundaryValues(float initialSource, float initialDestination, float amount)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);
        
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFunds(destination, amount));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFunds(destination, amount));
        }
        else if (initialSource <= amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFunds(destination, amount));
        }
        else
        {
            source.TransferFunds(destination, amount);
            
            Assert.Equal(source.Balance, initialSource - amount);
            Assert.Equal(destination.Balance, initialDestination + amount);
        }
    }
}