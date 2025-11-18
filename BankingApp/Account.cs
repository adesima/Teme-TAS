namespace BankingApp;

public class Account
{
    private float balance;
    private static float minBalance = 10;
    private bool isFrozen = false;
    private List<Transaction> _transactions;
    private INotifier _notifier;

    public Account()
    {
        balance = 0;   
        _transactions = new List<Transaction>();
    }

    public Account(float amount)
    {
        _transactions = new List<Transaction>();

        if (amount >= minBalance)
        {
            balance = amount;
            _transactions.Add(new Transaction(TransactionType.Deposit, amount, "Initial Deposit"));
        }
        else if (amount >= 0 && amount < minBalance)
            throw new MinimumBalanceException();
        else throw new NegativeAmountException();
    }
    
    public Account(float amount, INotifier notifier)
    {
        _transactions = new List<Transaction>();
        balance = amount;
        _notifier = notifier; 
        _transactions.Add(new Transaction(TransactionType.Deposit, amount, "Initial Deposit"));
    }
    
    public void Deposit(float amount)
    {
        if (isFrozen)
            throw new AccountFrozenException();
        
        if (amount > 0)
        {
            balance += amount;
            _transactions.Add(new Transaction(TransactionType.Deposit, amount));
        }
        else throw new NegativeAmountException();
    }
    
    public void Withdraw(float amount)
    {
        if (isFrozen)
            throw new AccountFrozenException();
        
        if (amount <= 0)
            throw new NegativeAmountException();

        if (balance - amount >= minBalance)
        {
            balance -= amount;
            _transactions.Add(new Transaction(TransactionType.Withdraw, amount));
            
            // AICI ESTE LOGICA PE CARE O VOM TESTA CU MOCK
            // Dacă retragem mult, trimitem notificare
            if (amount > 500 && _notifier != null)
            {
                _notifier.SendEmail($"Atentie! S-au retras {amount} RON din cont.");
            }
        }
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

    public void TransferFundsEurRon(Account destination, float amount, ICurrencyService currencyConverter)
    {
        float rate = currencyConverter.GetEurToRonRate();
        
        if (rate <= 0 )
            throw new IncorrectCurrecnyConverter();
        Withdraw(amount);
        destination.Deposit(amount * rate);
    }
    
    
    // ----------------------- Metode noi --------------------------
    public float GetBalanceWithFees(float amount, float feeRate)
    {
        // Calculează suma comisionului pe baza ratei (ex: 0.01 pentru 1%)
        float fee = amount * feeRate; 
    
        // Verifică dacă soldul final (după retragerea sumei ȘI a comisionului) 
        // respectă soldul minim.
        if (balance - amount - fee >= MinBalance)
        {
            return balance - amount - fee;
        }
        else if (balance - amount - fee > 0 && balance - amount - fee < MinBalance)
        {
            // Această metodă ar trebui să arunce aceeași excepție ca Withdraw 
            // pentru a testa coerența logicii.
            throw new MinimumBalanceException();
        }
        else
        {
            throw new NotEnoughFundsException();
        }
    }
    
    // Această metodă returnează o copie a istoricului
    public List<Transaction> GetTransactionHistory()
    {
        // Returnăm o listă nouă ca să nu poată cineva din exterior 
        // să șteargă tranzacții din istoric (încapsulare)
        return new List<Transaction>(_transactions);
    }
    // Opțional: O metodă care returnează istoricul ca un string frumos formatat
    public string GetHistoryReport()
    {
        var report = new System.Text.StringBuilder();
        report.AppendLine($"Transaction History for Account Balance: {balance}");
        report.AppendLine("------------------------------------------------");
        
        foreach (var t in _transactions)
        {
            report.AppendLine(t.ToString());
        }
        
        return report.ToString();
    }
    
    public void FreezeAccount()
    {
        isFrozen = true;
        // Opțional: adaugi un event in istoric
        _transactions.Add(new Transaction(TransactionType.Details, 0, "Account Frozen")); 
    }

    public void UnfreezeAccount()
    {
        isFrozen = false;
        _transactions.Add(new Transaction(TransactionType.Details, 0, "Account Unfrozen"));
    }
    
    public List<Transaction> GetTransactionsByDate(DateTime startDate, DateTime endDate)
    {
        List<Transaction> filteredList = new List<Transaction>();

        foreach (var t in _transactions)
        {
            // Verificăm dacă data tranzacției este în interval (inclusiv capetele)
            if (t.Date >= startDate && t.Date <= endDate)
            {
                filteredList.Add(t);
            }
        }

        if (filteredList.Count == 0)
        {
            // Opțional: Poți arunca o eroare sau returna o listă goală. 
            // De obicei, o listă goală este preferabilă (nu e o eroare că nu ai cumpărat nimic).
            return new List<Transaction>(); 
        }

        return filteredList;
    }

    public float Balance => balance;
    public static float MinBalance => minBalance;
    public bool IsFrozen => isFrozen;
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

public class AccountFrozenException : Exception
{
    public AccountFrozenException() : base("The account is frozen. No transactions are allowed.")
    {
    }
}

// public class TransferFailedException : Exception
// {
//     public TransferFailedException() : base("Trasfer failed. Funds have been restored.")
//     {
//     }
// }