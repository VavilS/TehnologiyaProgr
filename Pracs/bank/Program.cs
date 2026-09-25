namespace Bank
{
    internal class Program
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

            try
            {
                account2.MakeWithdrawal(99999999, DateTime.UtcNow, "XD");
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
