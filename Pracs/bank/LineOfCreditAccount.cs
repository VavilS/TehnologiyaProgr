namespace Bank;

/// <summary>
/// Создание кредитного счета
/// </summary>
public class LineOfCreditAccount:BankAccount
{
    /// <summary>
    /// Информация по счету
    /// </summary>
    /// <param name="name"></param>
    /// <param name="initialBalance"></param>
    /// <param name="creditLimit"></param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) 
        :base(name,initialBalance, -creditLimit) { }
   /// <summary>
   /// Увеличение суммы кредита
   /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest");
        }
    }

    /// <summary>
    /// Проверка возможности снятия средств
    /// </summary>
    /// <param name="isOverdrawn"></param>
    /// <returns></returns>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ?
       new Transaction(-20, DateTime.UtcNow, "apply overdraft") : default;

}
