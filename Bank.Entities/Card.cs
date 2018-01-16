using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string AccountNo { get; set; }
        public virtual Account Account { get; set; }

        public string Pin { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpirationDate { get; set; }

        public Card()
        {
            ExpirationDate = DateTime.Now.AddYears(3);
        }

        public Card( int clientId, string accountNo, string pin) : this()
        {
            ClientId = clientId;
            AccountNo = accountNo;
            Pin = pin;
        }
    }
}
