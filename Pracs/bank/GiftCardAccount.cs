namespace Bank;

/// <summary>
/// Класс счета подарочной карты 
/// </summary>
public class GiftCardAccount:BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;
    //monthlyDeposit - параметр по умолчанию(принимает 0)
    // при создании new GiftCardAccount("Stepan",1000);=>monthlyDeposit = 0
    // new GiftCardAccount("Stepan",1000,5000);=>monthlyDeposit = 5000
   /// <summary>
   /// Сумма, котора будет автоматически зачисляться на счет в начале каждого месяца
   /// </summary>
   /// <param name="name"></param>
   /// <param name="initialBalance"></param>
   /// <param name="monthlyDeposit"></param>
    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)=>_monthlyDeposit = monthlyDeposit;

   /// <summary>
   /// Выполняет ежемесячные операции по счету
   /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add montly deposit");
        }
    }
    //base.ToString() - вызов базовой реализации => реализаци из класса BankAccount
   /// <summary>
   /// Возвращает расширенную информацию о счете
   /// </summary>
   /// <returns></returns>
    public override string ToString()
    {
        return base.ToString() + $"monthly deposit: {_monthlyDeposit}";
    }



}
