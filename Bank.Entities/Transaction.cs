using System;
using Bank.Entities.Enums;

namespace Bank.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public string SrcAccountNo { get; set; }
        public virtual Account SrcAccount { get; set; }

        public string DestAccountNo { get; set; }
        public virtual Account DestAccount { get; set; }

        public DateTime Date { get; set; }

        public Decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public Transaction()
        {
            Date = DateTime.Now;
        }

        public Transaction( string srcAccountNo, string destAccountNo, Decimal amount, TransactionType type ) : this()
        {
            SrcAccountNo = srcAccountNo;
            DestAccountNo = destAccountNo;
            Amount = amount;
            Type = type;
        }
    }
}