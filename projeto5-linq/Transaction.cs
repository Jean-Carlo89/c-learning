public class Transaction
{
    public Guid Id { get; set; }

    public int AccountNumber { get; set; }
    public decimal Value { get; set; }

    public string Type { get; set; } // "Credit" or "Debit"
    public DateTime Date { get; set; }
    public string Description { get; set; }
}