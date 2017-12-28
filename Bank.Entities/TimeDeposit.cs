using System;

namespace Bank.Entities
{
    public class TimeDeposit : Account
    {
        //public int AccountNo { get; set; }
        //public virtual Account Account { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
