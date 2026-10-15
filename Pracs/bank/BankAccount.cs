using System.Text;

namespace Bank;

//BankAccount - потомок класса object => можно переопределить виртуальные методы находящиеся в obj
/// <summary>
/// Базовый класс для банковских счетов
/// </summary>
public class BankAccount
{
    private readonly decimal _minimumBalance;

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
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }
    /// <summary>
    /// Создает еще один счет, номер которого на 1 больше
    /// </summary>
    /// <param name="name"></param>
    /// <param name="initialBalance"></param>
    /// <param name="minimumBalance"></param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name; // this.Owner = name
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

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);
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

    public AccountStatus Status { get;  private set; }
}
