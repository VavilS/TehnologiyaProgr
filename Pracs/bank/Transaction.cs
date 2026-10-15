namespace Bank;

// record - состояние объектов этого класса нельзя изменить
// internal record Transaction
//{
//public decimal Amount { get; }
//public DateTime Date {  get; }
//public string Note { get; }
//publc Transaction(decimal Amount, DateTime Date, string Note)
//{
//    this.Amount = Amount;
//    this.Note = Note;
//    this.Date = Date;
//}
//}

/// <summary>
/// Выполнение операций со счетом
/// </summary>
/// <param name="Amount"></param>
/// <param name="Date"></param>
/// <param name="Note"></param>
public record Transaction(decimal Amount, DateTime Date, string Note);

