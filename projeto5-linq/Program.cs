using System.Runtime.InteropServices;

public class Program
{
    public static void Main(string[] args)
    {

        Account account = new Account(123456, 1000.00m, "Checking", "John Doe");
        List<Transaction> transactions = new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = 123456,
                Value = 1500.00m,
                Type = "Credit",
                Date = DateTime.Now.AddDays(-10),
                Description = "Salary Deposit"
            },
            new Transaction
            {
                Id = Guid.NewGuid(),
                AccountNumber = 123456,
                Value = 200.00m,
                Type = "Debit",
                Date = DateTime.Now.AddDays(-5),
                Description = "Grocery Shopping"
            }
        };

        account.addTransactions(transactions);

        account.getTransactionHistory();


    }
}