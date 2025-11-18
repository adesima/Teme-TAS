namespace BankingApp;

public enum TransactionType
{
    Deposit,
    Withdraw,
    Transfer,
    Details
}

public class Transaction
{
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public float Amount { get; set; }
    public string Details { get; set; }

    public Transaction(TransactionType type, float amount, string details = "")
    {
        Date = DateTime.Now; // Se pune automat data curentă
        Type = type;
        Amount = amount;
        Details = details;
    }
    
    // O metodă ajutătoare ca să afișezi frumos tranzacția
    public override string ToString()
    {
        return $"{Date}: {Type} -> {Amount} ({Details})";
    }
}