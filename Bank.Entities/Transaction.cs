using System;
using Bank.Entities.Enums;

namespace Bank.Entities
{
    public class Transaction
    {
        private static int _lastId = 0;

        public int Id { get; set; }

        public int SrcAccountNo { get; set; }
        public virtual Account SrcAccount { get; set; }

        public int DestAccountNo { get; set; }
        public virtual Account DestAccount { get; set; }

        public DateTime Date { get; set; }

        public Decimal Amount { get; set; }

        public TransactionType type { get; set; }

        public Transaction()
        {
            Id = _lastId;
            _lastId++;
        }
    }
}