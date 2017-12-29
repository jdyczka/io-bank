using System;

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

        public DateTime ExpirationDate { get; set; }
    }
}
