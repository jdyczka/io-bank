using Bank.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Entities
{
    public class TimeDeposit
    {
        private static int _lastId = 0;

        public int Id { get; set; }

        public string AccountNo { get; set; }
        public virtual Account Account { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }

        public TimeDeposit()
        {
            Id = _lastId;
            _lastId++;
        }
    }
}
