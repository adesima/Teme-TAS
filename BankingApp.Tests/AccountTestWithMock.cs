namespace BankingApp.Tests;

using Xunit;
using Moq;

public class AccountTestWithMock
{
    [Fact]
    public void Withdraw_MoreThan500_ShouldSendEmail()
    {
        // 1. ARRANGE (Pregătirea)
        
        // Creăm "Cascadorul" (Mock-ul) pentru interfața INotifier
        var mockNotifier = new Mock<INotifier>();

        // Creăm contul, dar îi dăm obiectul fals (mockNotifier.Object)
        Account acc = new Account(1000, mockNotifier.Object);

        // 2. ACT (Acțiunea)
        acc.Withdraw(600);

        // 3. ASSERT (Verificarea)
        
        // Verificăm dacă metoda SendEmail a fost apelată exact o dată
        // cu un mesaj care conține "600".
        mockNotifier.Verify(x => x.SendEmail(It.Is<string>(s => s.Contains("600"))), Times.Once);
    }

    [Fact]
    public void Withdraw_LessThan500_ShouldNOTSendEmail()
    {
        // 1. Arrange
        var mockNotifier = new Mock<INotifier>();
        Account acc = new Account(1000, mockNotifier.Object);

        // 2. Act
        acc.Withdraw(100); // Retragem puțin

        // 3. Assert
        // Verificăm că SendEmail NU a fost apelată niciodată
        mockNotifier.Verify(x => x.SendEmail(It.IsAny<string>()), Times.Never);
    }
}