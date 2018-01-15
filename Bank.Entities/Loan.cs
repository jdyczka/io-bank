using System;
using Bank.Entities.Enums;

namespace Bank.Entities
{
    public class Loan : Account
    {
        //public string AccountNo { get; set; }
        //public virtual Account Account { get; set; }

        public Decimal Amount { get; set; }

        public Decimal InstallmentAmount { get; set; }

        public int InstallmentFrequency { get; set; }

        public LoanStatus status { get; set; }

        public DateTime NextDueDate { get; set; }
    }
}
