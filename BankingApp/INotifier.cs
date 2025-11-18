namespace BankingApp;

public interface INotifier
{
    void SendEmail(string message);
}