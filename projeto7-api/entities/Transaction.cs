using System;
using System.Collections.Generic;

namespace BankSystem.Domain.Entities
{

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }

    public class Transaction
    {
        public int Id { get; private set; }
        public TransactionType Type { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public int? SourceAccountId { get; private set; }
        public int? DestinationAccountId { get; private set; }


        public Transaction(TransactionType type, decimal value, int? targetAccountId)
        {
            if (type == TransactionType.Transfer)
            {
                throw new ArgumentException("Cant be a transfer without both accounts. Use the other constructor.");
            }

            this.Type = type;
            this.Value = value;
            this.CreatedAt = DateTime.UtcNow;

            if (type == TransactionType.Deposit)
            {
                this.DestinationAccountId = targetAccountId;
                this.SourceAccountId = null;
            }
            else if (type == TransactionType.Withdrawal)
            {
                this.SourceAccountId = targetAccountId;
                this.DestinationAccountId = null;
            }
        }


        public Transaction(decimal value, int sourceAccountId, int destinationAccountId)
        {
            if (sourceAccountId == destinationAccountId)
            {
                throw new ArgumentException("The accounts cannot be the same for a transfer.");
            }

            this.Type = TransactionType.Transfer;
            this.Value = value;
            this.CreatedAt = DateTime.UtcNow;
            this.SourceAccountId = sourceAccountId;
            this.DestinationAccountId = destinationAccountId;
        }


        private Transaction() { }
    }
}