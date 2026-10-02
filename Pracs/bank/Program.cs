namespace Bank;

public class Program
{
    static void Main(string[] args)
    {
        BankAccount account1 = new BankAccount("Stepan", 100000);
        BankAccount account2 = new BankAccount("Seriy", 112345);
        Console.WriteLine($"account {account1.Balance} №{account1.Number} {account1.Owner}");
        Console.WriteLine($"account {account2.Balance} №{account2.Number} {account2.Owner}");

        account1.MakeDeposit(987654, DateTime.UtcNow, ":D");
        Console.WriteLine(account1.Balance);
        account1.MakeWithdrawal(999, DateTime.UtcNow, ":(");
        Console.WriteLine(account1.Balance);

        Console.WriteLine(account1.GetAccountHistory());

        try
        {
            account2.MakeWithdrawal(99999999, DateTime.UtcNow, "XD");
        }
        catch(InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        InterestEarningAccount interest = new("Stepan", 1000m);
        interest.MakeDeposit(100m, DateTime.UtcNow, ":P");
        interest.MakeWithdrawal(10m, DateTime.UtcNow, "T_T");
        interest.PerformMonthAndTransactions();

        //Console.WriteLine(interest.ToString);
        Console.WriteLine(interest);

        Console.WriteLine(interest.GetAccountHistory());

        GiftCardAccount giftCard = new("Stepan", 1000m, 5000m);
        giftCard.MakeDeposit(500m, DateTime.UtcNow, "O.O");
        giftCard.MakeWithdrawal(1000m, DateTime.UtcNow, "o_o");
        giftCard.PerformMonthAndTransactions();

        Console.WriteLine(giftCard);
        Console.WriteLine(giftCard.GetAccountHistory());
    }
}
