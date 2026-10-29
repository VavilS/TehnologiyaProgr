using System.Security.Cryptography.X509Certificates;

namespace Bank;

/// <summary>
/// Создание накопительного счета
/// </summary>
public class InterestEarningAccount : BankAccount
{
   /// <summary>
   /// Информация о счете
   /// </summary>
   /// <param name="name"></param>
   /// <param name="initialBalance"></param>
    public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance, AccountStatus.Regular)
    {

    }

  
   

    //override позволяет в дочернем классе определить новую реализацию метода PerformMonthAndTransactions

   /// <summary>
   /// Начисление процентов от текущего баланса накопительного счета
   /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
            AuditLog.Log(Number, "Interest", interest, DateTime.UtcNow);
        }


    }
}


