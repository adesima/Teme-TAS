namespace BankingApp;

public class Account
{
    private float balance;
    private static float minBalance = 10;

    public Account()
    {
        balance = 0;   
    }

    public Account(float amount)
    {
        if (amount >= minBalance)
            balance = amount;
        else if (amount >= 0 && amount < minBalance)
            throw new MinimumBalanceException();
        else throw new NegativeAmountException();
    }
    
    public void Deposit(float amount)
    { 
        if (amount > 0)
            balance += amount;
        else throw new NegativeAmountException();
    }
    
    public void Withdraw(float amount)
    {
        if (amount <= 0)
            throw new NegativeAmountException();
        
        if (balance - amount >= minBalance)
            balance -= amount;
        else if (balance - amount > 0 && balance - amount < minBalance)
            throw new MinimumBalanceException();
        else throw new NotEnoughFundsException();
    }
    
    public void TransferFunds(Account destination, float amount)
    {
        // try
        // {
            Withdraw(amount);
            destination.Deposit(amount);
        // }
        // catch // in cazul in care Withdraw reuseste dar Deposit arunca exceptie, pun banii inapoi in contul sursa
        // {
        //     Deposit(amount);
        //     throw;
        // }
    }

    public void TransferFundsEurRon(Account destination, float amount, float currencyConverter)
    {
        if (currencyConverter <= 0 )
            throw new IncorrectCurrecnyConverter();
        Withdraw(amount);
        destination.Deposit(amount * currencyConverter);
    }

    public float Balance => balance;

    public static float MinBalance => minBalance;
}

public class IncorrectCurrecnyConverter : Exception
{
    public IncorrectCurrecnyConverter() : base("Incorrect currency.")
    {
    }
}

public class NotEnoughFundsException : Exception
{
    public NotEnoughFundsException() : base("Not enough funds to complete the transfer.")
    {
    }
}

public class NegativeAmountException : Exception
{
    public NegativeAmountException() : base("You cannot deposit or withdraw a negative amount.")
    {
    }
}

public class MinimumBalanceException : Exception
{
    public MinimumBalanceException() : base("Your balance must be at least 10.")
    {
    }
}

// public class TransferFailedException : Exception
// {
//     public TransferFailedException() : base("Trasfer failed. Funds have been restored.")
//     {
//     }
// }