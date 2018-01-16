using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Entities
{
    public class TimeDeposit
    {
        public int Id { get; set; }

        public string AccountNo { get; set; }
        public virtual Account Account { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }

        public TimeDeposit()
        {
        }
    }
}
