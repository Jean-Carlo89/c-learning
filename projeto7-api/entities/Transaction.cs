using System;
using System.Collections.Generic;

namespace BankSystem.Domain.Entities
{

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        TransferOut,
        TransferIn
    }

    public class Transaction
    {
        public int Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public int? SourceAccountId { get; private set; }
        public int? DestinationAccountId { get; private set; }


        public Transaction(TransactionType type, decimal value, int sourceAccountId, int? destinationAccountId = null)
        {
            if (type == TransactionType.TransferOut && destinationAccountId == null)
            {
                throw new ArgumentException("TransferOut requires DestinationAccountId.");
            }
            if (type == TransactionType.TransferIn && sourceAccountId == null)
            {
                throw new ArgumentException("TransferIn requires SourceAccountId.");
            }

            this.Type = type;
            this.Value = value;
            this.CreatedAt = DateTime.UtcNow;
            this.SourceAccountId = sourceAccountId;
            this.DestinationAccountId = destinationAccountId;
        }

    }
}