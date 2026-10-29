using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
   public static class AuditLog
    {
        public static void Log(string accountNumber, string operationType, decimal amount, DateTime date)
        {
            Console.WriteLine($"[{date:yyyy-MM-dd HH:mm:ss}] Account {accountNumber}: Operation '{operationType}', Amount = {amount}");
        }
    }
}
