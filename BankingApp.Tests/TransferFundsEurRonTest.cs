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

        ICurrencyService myStub = new CurrencyConverter();
        
        source.TransferFundsEurRon(destination, 50, myStub);
        
        Assert.Equal(50, source.Balance);
        Assert.Equal(10f + 50f * myStub.GetEurToRonRate(), destination.Balance);
    }
    
    [Theory]
    [InlineData(100, 50, 10)]
    [InlineData(100, 50, -1)]
    [InlineData(100, 50, 95)]
    [InlineData(100, 50, 150)]
    public void TrasnferFundsEurRon_Fail_ThrowsException(float initialSource, float initialDestination, float amount)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);
        
        ICurrencyService myStub = new CurrencyConverter();

        if (myStub.GetEurToRonRate() <= 0)
        {
            Assert.Throws<IncorrectCurrecnyConverter>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        }
        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        }
        else if (initialSource < amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        }
    }
    
    // // ----------------------------------------- Domain Test -------------------------------------------
    [Theory]
    [InlineData(100, 50, 0.1f)]
    [InlineData(100, 50, 0)]
    [InlineData(100, 50, -0.1f)]
    [InlineData(100, 50, 89.9f)]
    [InlineData(100, 50, 90)]
    [InlineData(100, 50, 90.1f)]
    [InlineData(100, 50, 99.9f)]
    [InlineData(100, 50, 100)]
    [InlineData(100, 50, 100.1f)]
    public void TrasnferFundsEurRon_BoundaryValues(float initialSource, float initialDestination, float amount)
    {
        var source = new Account(initialSource);
        var destination = new Account(initialDestination);
        
        ICurrencyService myStub = new CurrencyConverter();

        if (amount <= 0)
        {
            Assert.Throws<NegativeAmountException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        } 
        else if (initialSource - amount > 0 && initialSource - amount < minBalance)
        {
            Assert.Throws<MinimumBalanceException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        }
        else if (initialSource <= amount)
        {
            Assert.Throws<NotEnoughFundsException>(() =>
                source.TransferFundsEurRon(destination, amount, myStub));
        }
        else
        {
            source.TransferFundsEurRon(destination, amount, myStub);
            
            Assert.Equal(source.Balance, initialSource - amount);
            Assert.Equal(destination.Balance, initialDestination + amount * myStub.GetEurToRonRate());
        }
    }
}