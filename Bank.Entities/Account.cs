using System;
using Bank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.Entities
{
    public class Account
    {
        private ICollection<Client> _clients;

        private static Random _random = new Random();
        private string _generateAccountNo()
        {
            string result = "2924901044";
            
            for ( int i = 0; i < 2; i++ )
            {
                result += _random.Next(10000000, 100000000);
            }
            
            return result;
        }

        [Key]
        public string AccountNo { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOpened { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateClosed { get; set; }

        public Decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public virtual AccountType Type { get; set; }

        public double InterestRate { get; set; }

        public virtual ICollection<Client> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        public Account()
        {
            AccountNo = _generateAccountNo();
            DateOpened = DateTime.Now;
            Balance = 0;
            _clients = new List<Client>();
        }

        public Account( string name, Currency currency, AccountType type, double interestRate) : this()
        {
            Name = name;
            Currency = currency;
            Type = type;
            InterestRate = interestRate;
        }
    }
}
