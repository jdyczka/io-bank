using Bank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using Bank.Entities.Enums;

namespace Bank.DataAccess
{
    public class BankDBInitializer : DropCreateDatabaseAlways<BankContext> {

        protected override void Seed (BankContext context)
        {
            var addresses = new List<Address>
            {
                new Address( "Polska", "Warszawa", "00-031", "Szpitalna", "8", "5" ),
                new Address( "Polska", "Lodz", "93-134", "Poznanska", "35", "18" ),
                new Address( "Polska", "Poznan", "60-102", "Ostatnia", "49", "5" ),
                new Address( "Polska", "Krakow", "30-006", "Wroclawska", "8", "2" )
            };
            context.Addresses.AddOrUpdate(a => new { a.Id, a.Country, a.City, a.PostalCode, a.Street, a.BuildingNr, a.AppartmentNr }, addresses.ToArray());

            var clients = new List<Client>
            {
                new Client( "Anna", "Blaszczyk", "62121055699", "ablaszczyk@onet.pl", 1 ),
                new Client( "Cezary", "Dabrowski", "53061158976", "cezarydbr@gmail.com", 2 ),
                new Client( "Edward", "Fronczewski", "89121051233", "efron@gmail.com", 2 ),
                new Client( "Grazyna", "Halicka", "91030509029", "graha@wp.pl", 1 )

            };
            context.Clients.AddOrUpdate(c => new { c.Id, c.FirstName, c.LastName, c.Pesel, c.Email, c.AddressId }, clients.ToArray());

            var employees = new List<Employee>
            {
                new Employee( "Kamila", "Laskowska", "62121012345", "k.laskowska@iobank.pl", 3, AuthLevel.Basic, "kamila" ),
                new Employee( "Mariusz", "Niewiadomski", "76020322556", "m.niewiadomski@iobank.pl", 3, AuthLevel.Basic, "mariusz"),
                new Employee( "Olga", "Pietrzak", "83120445589", "o.pietrzak@iobank.pl", 4, AuthLevel.Limited, "olga"),
                new Employee( "Robert", "Sobański", "90050624587", "r.sobanski@iobank.pl", 4, AuthLevel.Admin, "robert")

            };
            context.Employees.AddOrUpdate(e => new { e.Id, e.FirstName, e.LastName, e.Pesel, e.AddressId }, employees.ToArray());

            var accounts = new List<Account>
            {
                new Account { Balance=800, Currency=Currency.PLN, DateOpened=new DateTime(2010,8,18), InterestRate=4, Name="konto rozliczeniowe", Type=AccountType.Regular },
                new Account { Balance=1000, Currency=Currency.PLN, DateOpened=new DateTime(2008,5,20), InterestRate=3, Name="konto rozliczeniowe", Type=AccountType.Regular },
                new Account { Balance=1200, Currency=Currency.PLN, DateOpened=new DateTime(2013,11,28), InterestRate=5, Name="konto rozliczeniowe", Type=AccountType.Regular },
            };
            context.Accounts.AddOrUpdate(a => new { a.AccountNo, a.Balance, a.Currency, a.DateOpened, a.InterestRate, a.Name, a.Type }, accounts.ToArray());

            var cards = new List<Card>
            {
                new Card { Id=1, ClientId=1, ExpirationDate= new DateTime(2018,9,18), Pin="0123" },
                new Card { Id=2, ClientId=2, ExpirationDate= new DateTime(2018,5,20), Pin="1478" },
                new Card { Id=3, ClientId=3, ExpirationDate= new DateTime(2019,11,28), Pin="2589" },

            };
            context.Cards.AddOrUpdate(c => new { c.Id, c.AccountNo, c.ClientId, c.ExpirationDate, c.Pin, }, cards.ToArray());

            base.Seed(context);
        }
    }
}
