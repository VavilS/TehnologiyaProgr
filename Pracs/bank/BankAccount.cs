using System.Text;

namespace Bank;

//BankAccount - потомок класса object => можно переопределить виртуальные методы находящиеся в obj
/// <summary>
/// Базовый класс для банковских счетов
/// </summary>
public class BankAccount
{
    private readonly decimal _regularFee = 50m;
    public AccountStatus Status { get; protected init; }
    private readonly decimal  _minimumBalance;

    static private int s_accountNumberSeed = 1000000000;

    /// <summary>
    /// Номер счета
    /// </summary>
    public string Number { get; }
    /// <summary>
    /// Владелец счета
    /// </summary>
    public string Owner { get; private set; }
    /// <summary>
    /// Баланс, имеющийся на счете
    /// </summary>

    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }
    /// <summary>
    /// Список транзакций по счету
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();
    /// <summary>
    /// Создает новый счет с настройками минимального остатка
    /// </summary>
    /// <param name="name"></param>
    /// <param name="initialBalance"></param>
    public BankAccount(string name, decimal initialBalance, AccountStatus status) : this(name, initialBalance, status, 0) { }
    /// <summary>
    /// Создает еще один счет, номер которого на 1 больше
    /// </summary>
    /// <param name="name"></param>
    /// <param name="initialBalance"></param>
    /// <param name="minimumBalance"></param>
    public BankAccount(string name, decimal initialBalance, AccountStatus status, decimal minimumBalance)
    {
        Owner = name; // this.Owner = name
        Status = status;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

    }


    // пополнение
    /// <summary>
    /// Пополнение счета
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="date"></param>
    /// <param name="note"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// Исключение при попытке пополнить счет на отрицательное значение
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
        AuditLog.Log(Number, "Deposit", amount, date);
    }

    // снятие
    /// <summary>
    /// Снятие со счета
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="date"></param>
    /// <param name="note"></param>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransactions.Add(withdrawal);
        AuditLog.Log(Number, "Withdrawal", Math.Abs(amount), date);

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);
            AuditLog.Log(Number, $"Overdraft/{overdraftTransaction.Amount}", Math.Abs(overdraftTransaction.Amount), date);
        }
    }
    /// <summary>
    /// Минимально допустимый остаток после снятия средств
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// Исключение если баланс меньше чем сумма снятия 
    protected virtual Transaction? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        return default;
    }
    /// <summary>
    /// История всех транзакций 
    /// </summary>
    /// <returns></returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");

        }
        return report.ToString();
    }

    //Ключевое слово vitrual позволяет в дочернем классе предоставить другую реализацию метода PerformMonthAndTransactions

    /// <summary>
    /// Выполняет ежемесячные операции по счету
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        if (Status == AccountStatus.Regular)
        {
            decimal requiredBalance = _regularFee + _minimumBalance;
            if (Balance >= requiredBalance)
            {
                MakeWithdrawal(_regularFee, DateTime.UtcNow, "Monthly maintenance fee");
            }
        }
        if (Status == AccountStatus.Premium || Status == AccountStatus.VIP)
        {
            ApplyInterestOnHighBalance();
        }
    }

    private void ApplyInterestOnHighBalance()
    {
        const decimal threshold = 10000m;
        const decimal premiumRate = 0.01m;
        const decimal vipRate = 0.03m;
        if (Balance > threshold)
        {
            decimal rate = Status == AccountStatus.VIP ? vipRate : premiumRate;
            decimal interest = (Balance - threshold) * rate;
            if (interest > 0)
            {
                MakeDeposit(interest, DateTime.UtcNow, "Interest on balance > 10000");
            }
        }
    }
    private void ApplyCashbackOnWithdrawals()
    {
        var withdrawals = _allTransactions.Where(t => t.Amount < 0).ToList();
        foreach (var withdrawal in withdrawals)
        {
            decimal cashback = Math.Abs(withdrawal.Amount) * 0.05m;
            if (cashback > 0)
            {
                MakeDeposit(cashback, DateTime.UtcNow, $"Cashback for withdrawal {withdrawal.Note}");
            }
        }
    }

    // переопределяем метод, который унаследовали от object
    // этот метод должен возвращать строку с состоянием объекта

    /// <summary>
    /// Возвращает краткую информацию о счете
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    => $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of acount: {Number}";
    //{
    //    return $"Type: {GetType().Name}\tOwner: {Owner}\tNumber of acount: {Number}";

    //}

    public void TransferTo (BankAccount targetAccount, decimal amount, DateTime date, string note)
    {
        if (targetAccount == null) throw new ArgumentNullException(nameof(targetAccount));
        if (ReferenceEquals(this, targetAccount))
            throw new InvalidOperationException("Cannot transfer to the same account.");
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        var overdraftCheck = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        if (overdraftCheck is not null)
        {
            throw new InvalidOperationException("Not sufficient funds for this transfer");
        }
        var withdrawal = new Transaction(-amount, date, $"Transfer to {targetAccount.Number}: {note}");
        var deposit = new Transaction(+amount, date, $"Transfer from {Number}: {note}");
        targetAccount._allTransactions.Add(deposit);
        AuditLog.Log(targetAccount.Number, "Deposit", Math.Abs(deposit.Amount), date);
        _allTransactions.Add(withdrawal);
        AuditLog.Log(Number, "Withdrawal", Math.Abs(withdrawal.Amount), date);
    }
    

}
