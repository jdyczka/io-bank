using System.Collections.Generic;

namespace Bank.Entities
{
    public class Client
    {
        private ICollection<Account> _accounts;
        private ICollection<Card> _cards;

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pesel { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Account> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        public virtual ICollection<Card> Cards
        {
            get { return _cards; }
            set { _cards = value; }
        }

        public Client()
        {
            _accounts = new List<Account>();
            _cards = new List<Card>();
        }
    }
}
