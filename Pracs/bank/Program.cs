namespace Bank;

public class Program
{
    static void Main(string[] args)
    {
        BankAccount regularAcc = new BankAccount("RegularUser", 15000m, AccountStatus.Regular);
        BankAccount premiumAcc = new BankAccount("PremiumUser", 15000m, AccountStatus.Premium);
        BankAccount vipAcc = new BankAccount("VIPUser", 15000m, AccountStatus.VIP);
        Console.WriteLine($"account {regularAcc.Balance} №{regularAcc.Number} {regularAcc.Owner}");
        Console.WriteLine($"account {premiumAcc.Balance} №{premiumAcc.Number} {premiumAcc.Owner}");
        Console.WriteLine($"account {vipAcc.Balance} №{vipAcc.Number} {vipAcc.Owner}");

        BankAccount account1 = new BankAccount("Stepan", 100000, AccountStatus.Regular);
        BankAccount account2 = new BankAccount("Seriy", 112345, AccountStatus.Regular);
        Console.WriteLine($"account {account1.Balance} №{account1.Number} {account1.Owner}");
        Console.WriteLine($"account {account2.Balance} №{account2.Number} {account2.Owner}");

        account1.MakeDeposit(987654, DateTime.UtcNow, ":D");
        Console.WriteLine(account1.Balance);
        account1.MakeWithdrawal(999, DateTime.UtcNow, ":(");
        Console.WriteLine(account1.Balance);

        Console.WriteLine(account1.GetAccountHistory());

        regularAcc.MakeWithdrawal(999, DateTime.UtcNow, "Test debit");
        premiumAcc.MakeWithdrawal(999, DateTime.UtcNow, "Test debit");
        vipAcc.MakeWithdrawal(999, DateTime.UtcNow, "Test debit");

        try
        {
            account2.MakeWithdrawal(99999999, DateTime.UtcNow, "XD");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        InterestEarningAccount interest = new("Stepan", 1000m);
        interest.MakeDeposit(100m, DateTime.UtcNow, ":P");
        interest.MakeWithdrawal(10m, DateTime.UtcNow, "T_T");
        interest.PerformMonthAndTransactions();

        try
        {
            account1.TransferTo(account2, 300m, DateTime.UtcNow, "Repayment of debt");
            Console.WriteLine("Transfer succesful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Transfer failed: {ex.Message}");
        }
        Console.WriteLine("\n--- Account 1 History ---");
        Console.WriteLine(account1.GetAccountHistory());
        Console.WriteLine("\n--- Account 2 History ---");
        Console.WriteLine(account2.GetAccountHistory());

        //Console.WriteLine(interest.ToString);
        Console.WriteLine(interest);

        Console.WriteLine(interest.GetAccountHistory());

        LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Stepan", 0, 1000m);
        lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

        GiftCardAccount giftCard = new("Stepan", 1000m, 5000m);
        giftCard.MakeDeposit(500m, DateTime.UtcNow, "O.O");
        giftCard.MakeWithdrawal(1000m, DateTime.UtcNow, "o_o");
        giftCard.PerformMonthAndTransactions();

        Console.WriteLine(giftCard);
        Console.WriteLine(giftCard.GetAccountHistory());

        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(account1);
        accounts.Add(interest);
        accounts.Add(lineOfCredit);
        accounts.Add(giftCard);
        accounts.Add(regularAcc);
        accounts.Add(premiumAcc);
        accounts.Add(vipAcc);

        foreach (BankAccount account in accounts)
        {
            Console.WriteLine(account);
            account.PerformMonthAndTransactions();
            Console.WriteLine(account.GetAccountHistory());
        }
    }
}
