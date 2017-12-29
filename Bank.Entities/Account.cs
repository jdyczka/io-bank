using System;
using Bank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Bank.Entities
{
    public class Account
    {
        private ICollection<Client> _clients;

        [Key]
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime DateClosed { get; set; }
        public Decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public virtual AccountType Type { get; set; }
        public float InterestRate { get; set; }

        public virtual ICollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        public Account()
        {
            _clients = new List<Client>();
        }
    }
}
