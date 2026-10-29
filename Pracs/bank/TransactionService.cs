using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public static class TransactionService
    {
        public static void PerformTransfer(BankAccount sourceAccount, BankAccount targetAccount, decimal amount, DateTime date, string note)
        {
            ArgumentNullException.ThrowIfNull(sourceAccount);
            ArgumentNullException.ThrowIfNull(targetAccount);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

            
        }
    }
}
