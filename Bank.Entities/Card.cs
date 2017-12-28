using System;

namespace Bank.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int AccountNo { get; set; }
        public virtual Account Account { get; set; }

        public int Pin { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
