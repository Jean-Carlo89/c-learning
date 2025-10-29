using System.Globalization;

public class Account
{
    public int Number { get; set; }
    public decimal Balance { get; set; }
    public string Holder { get; set; }
    public string Type { get; set; }



    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    public Account(int Number, decimal Balance, string Type, string Holder)
    {
        this.Number = Number;
        this.Balance = Balance;
        this.Type = Type;
        this.Holder = Holder;

    }

    public void addTransaction(Transaction transaction)
    {
        this.Balance += transaction.Value;
        this.Transactions.Add(transaction);
    }

    public void addTransactions(List<Transaction> transactions)
    {
        foreach (var transaction in transactions)
        {
            addTransaction(transaction);
        }
    }

    public List<Transaction> getTransactionHistory()
    {
        printTransactionHistory(this.Transactions, this.Holder);
        return this.Transactions;

    }

    public static void printTransactionHistory(List<Transaction> transactions, string accountHolder)
    {
        CultureInfo brazilianCulture = CultureInfo.GetCultureInfo("pt-BR");
        Console.WriteLine("\n=======================================================");
        Console.WriteLine($" EXTRATO DE TRANSAÇÕES - Titular: {accountHolder}");
        Console.WriteLine("=======================================================");


        foreach (var t in transactions)
        {

            string formattedDAte = t.Date.ToString("dd/MM/yyyy", brazilianCulture);


            decimal printedValue = (t.Type.ToLower() == "debit") ? -t.Value : t.Value;
            string stringfiedValueFormat = printedValue.ToString("N2");


            Console.WriteLine("{0, -12} | {1, -10} | {2, 12} | {3, -30}",
                              formattedDAte,
                              t.Type,
                              stringfiedValueFormat,
                              t.Description);
        }

        Console.WriteLine("=======================================================\n");
    }
}