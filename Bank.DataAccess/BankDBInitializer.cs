using Bank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity;


namespace Bank.DataAccess
{
    public class BankDBInitializer : DropCreateDatabaseAlways<BankContext> {

        protected override void Seed (BankContext context)
        {
            var addresses = new List<Address>
            {
                new Address { Id=1, Country="Polska",City="Warszawa",PostalCode="00-031",Street="Szpitalna",BuildingNr="8",AppartmentNr="5" },
                new Address { Id=2, Country = "Polska", City = "Lodz", PostalCode = "93-134", Street = "Poznanska", BuildingNr = "35", AppartmentNr = "18" },
                new Address { Id=3, Country = "Polska", City = "Poznan", PostalCode = "60-102", Street = "Ostatnia", BuildingNr = "49", AppartmentNr = "5" },
                new Address { Id=4, Country = "Polska", City = "Krakow", PostalCode = "30-006", Street = "Wroclawska", BuildingNr = "8", AppartmentNr = "2" }
            };
            context.Addresses.AddOrUpdate(a => new { a.Id, a.Country, a.City, a.PostalCode, a.Street, a.BuildingNr, a.AppartmentNr }, addresses.ToArray());

            var clients = new List<Client>
            {
                new Client { Id=1, FirstName="Anna",LastName="Blaszczyk",Pesel="62121055699",Email="ablaszczyk@onet.pl",AddressId=1 },
                new Client { Id=2, FirstName="Cezary",LastName="Dabrowski",Pesel="53061158976",Email="cezarydbr@gmail.com",AddressId=2 },
                new Client { Id=3, FirstName="Edward",LastName="Fronczewski",Pesel="89121051233",Email="efron@gmail.com",AddressId=2 },
                new Client { Id=4, FirstName="Grazyna",LastName="Halicka",Pesel="91030509029",Email="graha@wp.pl",AddressId=1 }

            };
            context.Clients.AddOrUpdate(c => new { c.Id, c.FirstName, c.LastName, c.Pesel, c.Email, c.AddressId }, clients.ToArray());

            var employees = new List<Employee>
            {
                new Employee { Id=1, FirstName="Kamila",LastName="Laskowska",Pesel="62121012345", AddressId=3, AuthLevel=Entities.Enums.AuthLevel.Basic },
                new Employee { Id=2, FirstName="Mariusz",LastName="Niewiadomski",Pesel="76020322556", AddressId=3, AuthLevel=Entities.Enums.AuthLevel.Basic},
                new Employee { Id=3, FirstName="Olga",LastName="Pietrzak",Pesel="83120445589", AddressId=4, AuthLevel=Entities.Enums.AuthLevel.Limited},
                new Employee { Id=4, FirstName="Robert",LastName="Sobański",Pesel="90050624587", AddressId=4, AuthLevel=Entities.Enums.AuthLevel.Admin}

            };
            context.Employees.AddOrUpdate(e => new { e.Id, e.FirstName, e.LastName, e.Pesel, e.AddressId }, employees.ToArray());

            //var accounts = new List<Account>
            //{
            //    new Account { AccountNo="29249010440000320094007370", Balance=800, Currency=Entities.Enums.Currency.PLN, DateOpened=new DateTime(2010,8,18), InterestRate=4, Name="konto rozliczeniowe", Type=Entities.Enums.AccountType.Regular },
            //    new Account { AccountNo="29249010440000320023546897", Balance=1000, Currency=Entities.Enums.Currency.PLN, DateOpened=new DateTime(2008,5,20), InterestRate=3, Name="konto rozliczeniowe", Type=Entities.Enums.AccountType.Regular },
            //    new Account { AccountNo="29249010440000320257892354", Balance=1200, Currency=Entities.Enums.Currency.PLN, DateOpened=new DateTime(2013,11,28), InterestRate=5, Name="konto rozliczeniowe", Type=Entities.Enums.AccountType.Regular },
            //};
            //context.Accounts.AddOrUpdate(a => new { a.AccountNo, a.Balance, a.Currency, a.DateOpened, a.InterestRate, a.Name, a.Type }, accounts.ToArray());

            //var cards = new List<Card>
            //{
            //    new Card { Id=1, AccountNo="29249010440000320094007370", ClientId=1, ExpirationDate= new DateTime(2018,9,18), Pin="0123" },
            //    new Card { Id=2, AccountNo="29249010440000320023546897", ClientId=2, ExpirationDate= new DateTime(2018,5,20), Pin="1478" },
            //    new Card { Id=3, AccountNo="29249010440000320257892354", ClientId=3, ExpirationDate= new DateTime(2019,11,28), Pin="2589" },

            //};
            //context.Cards.AddOrUpdate(c => new { c.Id, c.AccountNo, c.ClientId, c.ExpirationDate, c.Pin, }, cards.ToArray());

            base.Seed(context);
        }
    }
}
