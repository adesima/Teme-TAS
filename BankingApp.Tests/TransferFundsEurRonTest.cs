namespace BankingApp.Tests;

using Xunit;

public class TransferFundsTestEurRon
{
    private Account account;
    private float minBalance = Account.MinBalance;
    
    // ------------------------------------------ Unit Tests -------------------------------------
    [Fact]
    public void TransferFundsEurRon_Pass()
    {
        var source = new Account(100);
        var destination = new Account(10);
        
        source.TransferFundsEurRon(destination, 50, 5.08f);
        
        Assert.Equal(50, source.Balance);
        Assert.Equal(10f + 50f * 5.08f, destination.Balance);
    }
    
    [Theory]
    [InlineData(100, 50, 10, -2)]
    [InlineData(100, 50, -1, 5.08f)]
    [InlineData(100, 50, 95, 5.08f)]
    [InlineData(100, 50, 150, 5.08f)]
    public void TrasnferFundsEurRon_Fail_ThrowsException(float initialSource, float initialDestination, float amount, float currencyConverter)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);

        if (currencyConverter <= 0)
        {
            Assert.Throws<IncorrectCurrecnyConverter>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        }
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        }
        else if (initialSource < amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        }
    }
    
    // // ----------------------------------------- Domain Test -------------------------------------------
    [Theory]
    [InlineData(100, 50, 0.1f, 5.08f)]
    [InlineData(100, 50, 0, 5.08f)]
    [InlineData(100, 50, -0.1f, 5.08f)]
    [InlineData(100, 50, 89.9f, 5.08f)]
    [InlineData(100, 50, 90, 5.08f)]
    [InlineData(100, 50, 90.1f, 5.08f)]
    [InlineData(100, 50, 99.9f, 5.08f)]
    [InlineData(100, 50, 100, 5.08f)]
    [InlineData(100, 50, 100.1f, 5.08f)]
    public void TrasnferFundsEurRon_BoundaryValues(float initialSource, float initialDestination, float amount, float currencyConverter)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);

        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        }
        else if (initialSource <= amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFundsEurRon(destination, amount, currencyConverter));
        }
        else
        {
            source.TransferFundsEurRon(destination, amount, currencyConverter);
            
            Assert.Equal(source.Balance, initialSource - amount);
            Assert.Equal(destination.Balance, initialDestination + amount * currencyConverter);
        }
    }
}