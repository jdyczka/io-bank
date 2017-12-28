namespace Bank.DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Bank.Entities;

    public sealed class Configuration : DbMigrationsConfiguration<Bank.DataAccess.BankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Bank.DataAccess.BankContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
#if true
            var addresses = new List<Address>
            {
                new Address { Id=1, Country="Polska",City="Warszawa",PostalCode="00-031",Street="Szpitalna",BuildingNr="8",AppartmentNr="5" },
                new Address { Id=2, Country = "Polska", City = "Lodz", PostalCode = "93-134", Street = "Poznanska", BuildingNr = "35", AppartmentNr = "18" }
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
                new Employee { Id=1, FirstName="Kamila",LastName="Laskowska",Pesel="62121012345", AddressId=1 },
                new Employee { Id=1, FirstName="Mariusz",LastName="Niewiadomski",Pesel="76020322556", AddressId=1 }

            };
            context.Employees.AddOrUpdate(e => new { e.Id, e.FirstName, e.LastName, e.Pesel, e.AddressId }, employees.ToArray());
#endif
        }
    }
}

