namespace Bank;

public class GiftCardAccount:BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;
    //monthlyDeposit - параметр по умолчанию(принимает 0)
    // при создании new GiftCardAccount("Stepan",1000);=>monthlyDeposit = 0
    // new GiftCardAccount("Stepan",1000,5000);=>monthlyDeposit = 5000
    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)=>_monthlyDeposit = monthlyDeposit;

    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add montly deposit");
        }
    }
    //base.ToString() - вызов базовой реализации => реализаци из класса BankAccount
    public override string ToString()
    {
        return base.ToString() + $"monthly deposit: {_monthlyDeposit}";
    }



}
