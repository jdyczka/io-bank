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
    //public class BankDBInitializer : DropCreateDatabaseAlways<BankContext> 
    public class BankDBInitializer : CreateDatabaseIfNotExists<BankContext>
    { 
            protected override void Seed (BankContext context)
        {
            var addresses = new List<Address>
            {
                new Address( "Polska", "Warszawa",  "00-031", "ul. Szpitalna", "8", "5" ),
                new Address( "Polska", "Łódź",      "93-134", "ul. Poznańska", "35", "18" ),
                new Address( "Polska", "Poznań",    "60-102", "ul. Ostatnia", "49", "5" ),
                new Address( "Polska", "Kraków",    "30-006", "ul. Wrocławska", "8", "2" ),
                new Address( "Polska", "Warszawa",  "00-001", "Aleje Jerozolimskie", "23", "7" ),
                new Address( "Polska", "Łódź",      "91-350", "ul. Andrzeja Struga", "12", "3" ),
                new Address( "Polska", "Gdańsk",    "80-029", "ul. Długa", "15", "21" ),
                new Address( "Polska", "Zamość",    "20-538", "al. Wojska Polskiego", "16", "-" ),
                new Address( "Polska", "Warszawa",  "00-018", "ul. Ujazdowska", "57", "34" ),
                new Address( "Polska", "Łódź",      "92-159", "Łagiewnicka", "12", "-" ),
                new Address( "Polska", "Szczecin",  "70-005", "al.Wyzwolenia", "21", "4" ),
                new Address( "Polska", "Kraków",    "30-006", "al. Adama Mickiewicza", "14", "22" )
            };
            context.Addresses.AddOrUpdate(a => new { a.Id, a.Country, a.City, a.PostalCode, a.Street, a.BuildingNr, a.AppartmentNr }, addresses.ToArray());
            context.SaveChanges();

            var clients = new List<Client>
            {
                new Client( "Anna",     "Blaszczyk",    "62121055699", "ablaszczyk@onet.pl",        addresses[0].Id ),
                new Client( "Cezary",   "Dabrowski",    "53061158976", "cezarydbr@gmail.com",       addresses[1].Id ),
                new Client( "Edward",   "Fronczewski",  "89121051233", "efron@gmail.com",           addresses[2].Id ),
                new Client( "Grazyna",  "Halicka",      "91030509029", "ioproject2017pl@gmail.com", addresses[3].Id ),
                new Client( "Jan",      "Kowalski",     "75122577890", "j.kowalski@wp.pl",          addresses[4].Id ),
                new Client( "Katarzyna","Borowicka",    "87031488764", "kate87@buziaczek.pl",       addresses[5].Id )
            };
            context.Clients.AddOrUpdate(c => new { c.Id, c.FirstName, c.LastName, c.Pesel, c.Email, c.AddressId }, clients.ToArray());
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee( "Kamila",     "Laskowska",    "62121012345", "k.laskowska@iobank.pl",     addresses[6].Id,  AuthLevel.Basic,   "kamila" ),
                new Employee( "Mariusz",    "Niewiadomski", "76020322556", "m.niewiadomski@iobank.pl",  addresses[7].Id,  AuthLevel.Basic,   "mariusz"),
                new Employee( "Adrian",     "Nowak",        "58052490753", "a.nowak@iobank.pl",         addresses[8].Id,  AuthLevel.Limited, "adrian"),
                new Employee( "Ewelina",    "Lisowska",     "91072890751", "e.lisowska@iobank.pl",      addresses[9].Id,  AuthLevel.Limited, "ewelka"),
                new Employee( "Olga",       "Pietrzak",     "83120445589", "o.pietrzak@iobank.pl",      addresses[10].Id, AuthLevel.Limited, "olga"),
                new Employee( "Robert",     "Sobański",     "90050624587", "r.sobanski@iobank.pl",      addresses[11].Id, AuthLevel.Admin,   "robert"),
                new Employee( "Admin",      "Admin",        "00000000000", "admin",                     addresses[0].Id,  AuthLevel.Admin,   "admin")
            };
            context.Employees.AddOrUpdate(e => new { e.Id, e.FirstName, e.LastName, e.Pesel, e.AddressId, e.AuthLevel, e.Password }, employees.ToArray());
            context.SaveChanges();

            var accounts = new List<Account>
            {
                // client 0
                new Account { Balance=800,   Currency=Currency.PLN, DateOpened=new DateTime(2010, 8,18), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Regular },
                new Account { Balance=4253,  Currency=Currency.PLN, DateOpened=new DateTime(2015, 5,20), InterestRate=0.01, Name="konto oszczędnościowe",   Type=AccountType.Regular },
                // client 1, 2
                new Account { Balance=1466,  Currency=Currency.PLN, DateOpened=new DateTime(2013,11,28), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Regular },
                new Account { Balance=3457,  Currency=Currency.PLN, DateOpened=new DateTime(2014, 1, 4), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Regular },
                new Account { Balance=6578,  Currency=Currency.PLN, DateOpened=new DateTime(2016, 3,19), InterestRate=0,    Name="konto współdzielone",     Type=AccountType.Regular },
                // client 3, 4
                new Account { Balance=14733, Currency=Currency.PLN, DateOpened=new DateTime(2007,10,15), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Regular },
                new Account { Balance=24889, Currency=Currency.PLN, DateOpened=new DateTime(2009, 9, 9), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Gold },
                new Account { Balance=285693,Currency=Currency.PLN, DateOpened=new DateTime(2011, 3,21), InterestRate=0.03, Name="konto współdzielone",     Type=AccountType.Platinum },
                // client 5
                new Account { Balance=732,   Currency=Currency.PLN, DateOpened=new DateTime(2011, 8,14), InterestRate=0,    Name="konto osobiste",          Type=AccountType.Regular },
                new Account { Balance=57245, Currency=Currency.PLN, DateOpened=new DateTime(2012, 7,24), InterestRate=0.025,Name="konto oszczędnościowe",   Type=AccountType.Gold }
            };
            context.Accounts.AddOrUpdate(a => new { a.AccountNo, a.Balance, a.Currency, a.DateOpened, a.InterestRate, a.Name, a.Type }, accounts.ToArray());
            context.SaveChanges();

            var loanAccounts = new List<Account>
            {
                new Account { Balance=-4500,  Currency=Currency.PLN, DateOpened=new DateTime(2016,12,29), InterestRate=0.03, Name="Kredyt osobisty",         Type=AccountType.Regular },
                new Account { Balance=-35000, Currency=Currency.PLN, DateOpened=new DateTime(2017,10,11), InterestRate=0.03, Name="Kredyt osobisty",         Type=AccountType.Regular }
            };
            context.Accounts.AddOrUpdate(a => new { a.AccountNo, a.Balance, a.Currency, a.DateOpened, a.InterestRate, a.Name, a.Type }, accounts.ToArray());
            context.SaveChanges();

            var timeDepositAccounts = new List<Account>
            {
                new Account { Balance=15000, Currency=Currency.PLN, DateOpened=new DateTime(2018, 1, 2), InterestRate=0.03, Name="Lokata osobista",         Type=AccountType.Regular },
                new Account { Balance=100000,Currency=Currency.PLN, DateOpened=new DateTime(2018, 1, 5), InterestRate=0.04, Name="Lokata osobista",         Type=AccountType.Platinum },
                new Account { Balance=50000, Currency=Currency.PLN, DateOpened=new DateTime(2017,12,30), InterestRate=0.035,Name="Lokata osobista",         Type=AccountType.Gold }
            };
            context.Accounts.AddOrUpdate(a => new { a.AccountNo, a.Balance, a.Currency, a.DateOpened, a.InterestRate, a.Name, a.Type }, accounts.ToArray());
            context.SaveChanges();

            clients[0].Accounts.Add(accounts[0]);
            clients[0].Accounts.Add(accounts[1]);
            clients[0].Accounts.Add(loanAccounts[0]);
            clients[1].Accounts.Add(accounts[2]);
            clients[1].Accounts.Add(accounts[4]);
            clients[1].Accounts.Add(loanAccounts[1]);
            clients[2].Accounts.Add(accounts[3]);
            clients[2].Accounts.Add(accounts[4]);
            clients[2].Accounts.Add(timeDepositAccounts[0]);
            clients[3].Accounts.Add(accounts[5]);
            clients[3].Accounts.Add(accounts[7]);
            clients[4].Accounts.Add(accounts[5]);
            clients[4].Accounts.Add(accounts[7]);
            clients[4].Accounts.Add(timeDepositAccounts[1]);
            clients[5].Accounts.Add(accounts[8]);
            clients[5].Accounts.Add(accounts[9]);
            clients[5].Accounts.Add(timeDepositAccounts[2]);
            context.SaveChanges();

            var loans = new List<Loan>
            {
                new Loan { AccountNo=loanAccounts[0].AccountNo, Amount=10000, InstallmentAmount=500,  InstallmentFrequency=12, status=LoanStatus.Overdue,  NextDueDate=new DateTime(2017,12,29) },
                new Loan { AccountNo=loanAccounts[1].AccountNo, Amount=50000, InstallmentAmount=5000, InstallmentFrequency=12, status=LoanStatus.UpToDate, NextDueDate=new DateTime(2018, 2,11) }
            };
            context.Loans.AddOrUpdate(l => new { l.Id, l.AccountNo, l.Amount, l.InstallmentAmount, l.InstallmentFrequency, l.status, l.NextDueDate }, loans.ToArray());
            context.SaveChanges();

            var timeDeposits = new List<TimeDeposit>
            {
                new TimeDeposit { AccountNo = timeDepositAccounts[0].AccountNo, ExpirationDate = timeDepositAccounts[0].DateOpened.AddMonths(3) },
                new TimeDeposit { AccountNo = timeDepositAccounts[1].AccountNo, ExpirationDate = timeDepositAccounts[1].DateOpened.AddMonths(2) },
                new TimeDeposit { AccountNo = timeDepositAccounts[2].AccountNo, ExpirationDate = timeDepositAccounts[2].DateOpened.AddMonths(6) }
            };
            context.TimeDeposits.AddOrUpdate(t => new { t.Id, t.AccountNo, t.ExpirationDate }, timeDeposits.ToArray());
            context.SaveChanges();

            var cards = new List<Card>
            {
                new Card { ClientId=clients[0].Id, Account=accounts[0], ExpirationDate= new DateTime(2018,9,18), Pin="0123" },
                new Card { ClientId=clients[3].Id, Account=accounts[7], ExpirationDate= new DateTime(2019,5,20), Pin="1478" },
                new Card { ClientId=clients[4].Id, Account=accounts[7], ExpirationDate= new DateTime(2019,5,20), Pin="2589" },

            };
            context.Cards.AddOrUpdate(c => new { c.Id, c.AccountNo, c.ClientId, c.ExpirationDate, c.Pin, }, cards.ToArray());
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
