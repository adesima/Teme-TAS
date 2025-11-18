namespace BankingApp.ConsoleUI;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("=== START BANKING APP TEST ===\n");
//
//             try
//             {
//                 // 1. Creăm un cont nou cu 100 RON
//                 Console.WriteLine("-> Creare cont A cu 100 RON...");
//                 Account accountA = new Account(100);
//                 Console.WriteLine($"Sold curent A: {accountA.Balance}");
//
//                 // 2. Facem o depunere
//                 Console.WriteLine("\n-> Depunere 50 RON in A...");
//                 accountA.Deposit(50);
//                 Console.WriteLine($"Sold curent A: {accountA.Balance}");
//
//                 // 3. Facem o retragere
//                 Console.WriteLine("\n-> Retragere 20 RON din A...");
//                 accountA.Withdraw(20);
//                 Console.WriteLine($"Sold curent A: {accountA.Balance}");
//
//                 // 4. Testăm metoda GetBalanceWithFees (fără a modifica soldul real)
//                 // Să zicem că vrem să vedem cât ar rămâne dacă retragem 10 RON cu 10% comision
//                 Console.WriteLine("\n-> Simulare retragere 10 RON cu comision 10%...");
//                 float simulatedBalance = accountA.GetBalanceWithFees(10, 0.10f); 
//                 Console.WriteLine($"Dacă am retrage, am rămâne cu: {simulatedBalance}");
//                 Console.WriteLine($"Soldul REAL a rămas neschimbat: {accountA.Balance}");
//
//                 // 5. Creăm al doilea cont pentru transfer
//                 Console.WriteLine("\n-> Creare cont B cu 20 RON...");
//                 Account accountB = new Account(20);
//
//                 // 6. Transferăm bani din A în B
//                 Console.WriteLine("\n-> Transfer 30 RON din A in B...");
//                 accountA.TransferFunds(accountB, 30);
//                 Console.WriteLine($"Sold A: {accountA.Balance}");
//                 Console.WriteLine($"Sold B: {accountB.Balance}");
//
//                 // 7. AFISARE ISTORIC (Dacă ai implementat metoda GetHistoryReport)
//                 Console.WriteLine("\n=== RAPORT ISTORIC CONT A ===");
//                 // Dacă ai făcut metoda cu string:
//                 Console.WriteLine(accountA.GetHistoryReport());
//                 
//                 // SAU, dacă ai făcut doar metoda care returnează List<Transaction>:
//                 /*
//                 var history = accountA.GetTransactionHistory();
//                 foreach(var t in history)
//                 {
//                     Console.WriteLine(t.ToString());
//                 }
//                 */
//             }
//             catch (Exception ex)
//             {
//                 // Aici prindem orice eroare (Fonduri insuficiente, Sold minim, etc.)
//                 Console.WriteLine($"\n[EROARE]: {ex.Message}");
//             }
//
//             Console.WriteLine("\n=== TEST FINALIZAT ===");
//             // Această linie ține consola deschisă până apeși o tastă (util în unele versiuni vechi de VS)
//             Console.ReadLine(); 
//     }
// }

internal class Program
    {
        // Contul principal pe care îl vom manipula
        static Account? currentAccount = null;

        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                // Curățăm consola la fiecare pas pentru un aspect "fresh"
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("==========================================");
                Console.WriteLine("       BANCAR APP - MAIN MENU");
                Console.WriteLine("==========================================");
                Console.ResetColor();
                
                // Afișăm statusul contului curent (dacă există)
                if (currentAccount != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string status = currentAccount.IsFrozen ? "BLOCAT (FROZEN)" : "ACTIV";
                    Console.WriteLine($" CONT ACTIV | Sold: {currentAccount.Balance} RON | Status: {status}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" NICIO CONT SELECTAT. Te rog creează un cont.");
                }
                Console.ResetColor();
                Console.WriteLine("------------------------------------------");

                // --- MENIUL DE OPȚIUNI ---
                Console.WriteLine("1.  Creare Cont Nou (Constructor)");
                Console.WriteLine("2.  Depunere (Deposit)");
                Console.WriteLine("3.  Retragere (Withdraw)");
                Console.WriteLine("4.  Transfera Fonduri (Catre alt cont)");
                Console.WriteLine("5.  Transfera EUR->RON (Convertor)");
                Console.WriteLine("6.  Simulare Sold cu Taxe (GetBalanceWithFees)");
                Console.WriteLine("7.  Istoric Tranzactii (Toate)");
                Console.WriteLine("8.  Istoric Tranzactii (Filtru Data)");
                Console.WriteLine("9.  Blocheaza Cont (Freeze)");
                Console.WriteLine("10. Deblocheaza Cont (Unfreeze)");
                Console.WriteLine("0.  IESIRE");
                Console.WriteLine("------------------------------------------");
                Console.Write("Alege o optiune: ");

                string option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            CreateAccount();
                            break;
                        case "2":
                            DoDeposit();
                            break;
                        case "3":
                            DoWithdraw();
                            break;
                        case "4":
                            DoTransfer();
                            break;
                        case "5":
                            DoTransferEurRon();
                            break;
                        case "6":
                            CheckFees();
                            break;
                        case "7":
                            ShowHistory();
                            break;
                        case "8":
                            ShowHistoryByDate();
                            break;
                        case "9":
                            Freeze();
                            break;
                        case "10":
                            Unfreeze();
                            break;
                        case "0":
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Opțiune invalidă!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[EROARE]: {ex.Message}");
                    Console.ResetColor();
                }

                if (isRunning)
                {
                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                    Console.ReadKey();
                }
            }
        }

        // --- METODE AJUTATOARE PENTRU MENIU ---

        static void CreateAccount()
        {
            Console.Write("Introdu suma initiala: ");
            float amount = float.Parse(Console.ReadLine());
            currentAccount = new Account(amount);
            Console.WriteLine("Cont creat cu succes!");
        }

        static void DoDeposit()
        {
            CheckAccountExists();
            Console.Write("Suma de depus: ");
            float amount = float.Parse(Console.ReadLine());
            currentAccount.Deposit(amount);
            Console.WriteLine("Depunere reusita!");
        }

        static void DoWithdraw()
        {
            CheckAccountExists();
            Console.Write("Suma de retras: ");
            float amount = float.Parse(Console.ReadLine());
            currentAccount.Withdraw(amount);
            Console.WriteLine("Retragere reusita!");
        }

        static void DoTransfer()
        {
            CheckAccountExists();
            // Cream un cont destinatie "dummy" pentru test
            Account destinatie = new Account(50);
            
            Console.Write("Suma de transferat catre Contul B: ");
            float amount = float.Parse(Console.ReadLine());
            
            currentAccount.TransferFunds(destinatie, amount);
            Console.WriteLine($"Transfer reusit! Soldul tau: {currentAccount.Balance} | Sold Destinatie: {destinatie.Balance}");
        }

        static void DoTransferEurRon()
        {
            CheckAccountExists();
            Account destinatie = new Account(50);

            Console.Write("Suma in EUR de transferat: ");
            float amount = float.Parse(Console.ReadLine());

            // Console.Write("Curs valutar (ex: 4.9): ");
            // float rate = float.Parse(Console.ReadLine());
            
            ICurrencyService currencyConverter = new CurrencyConverter();

            currentAccount.TransferFundsEurRon(destinatie, amount, currencyConverter);
            Console.WriteLine($"Transfer EUR->RON reusit! Destinatarul a primit: {amount * currencyConverter.GetEurToRonRate()} RON");
        }

        static void CheckFees()
        {
            CheckAccountExists();
            Console.Write("Suma pentru simulare retragere: ");
            float amount = float.Parse(Console.ReadLine());
            
            Console.Write("Comision (ex: 0.1 pentru 10%): ");
            float fee = float.Parse(Console.ReadLine());

            float result = currentAccount.GetBalanceWithFees(amount, fee);
            Console.WriteLine($"Daca retragi acum, soldul ar ramane: {result} (Soldul real nu a fost atins)");
        }

        static void ShowHistory()
        {
            CheckAccountExists();
            // Dacă ai implementat metoda GetHistoryReport (care returneaza string)
            // Console.WriteLine(currentAccount.GetHistoryReport());

            // Dacă ai doar metoda care returnează List<Transaction>:
            var list = currentAccount.GetTransactionHistory(); // sau numele metodei tale
            Console.WriteLine("\n--- ISTORIC COMPLET ---");
            foreach (var t in list)
            {
                Console.WriteLine(t.ToString());
            }
        }

        static void ShowHistoryByDate()
        {
            CheckAccountExists();
            Console.WriteLine("Format data: yyyy-mm-dd (ex: 2023-10-25)");
            
            Console.Write("Data Inceput: ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("Data Sfarsit: ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            var list = currentAccount.GetTransactionsByDate(start, end);
            Console.WriteLine($"\n--- ISTORIC FILTRAT ({list.Count} tranzactii) ---");
            foreach (var t in list)
            {
                Console.WriteLine(t.ToString());
            }
        }

        static void Freeze()
        {
            CheckAccountExists();
            currentAccount.FreezeAccount();
            Console.WriteLine("Contul a fost BLOCAT.");
        }

        static void Unfreeze()
        {
            CheckAccountExists();
            currentAccount.UnfreezeAccount();
            Console.WriteLine("Contul a fost DEBLOCAT.");
        }

        // Validare simplă ca să nu crpe programul dacă nu avem cont
        static void CheckAccountExists()
        {
            if (currentAccount == null)
                throw new Exception("Nu ai creat niciun cont! Alege optiunea 1 mai intai.");
        }
    }