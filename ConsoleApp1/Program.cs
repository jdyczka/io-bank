using System;
using System.Linq;
using Bank.DataAccess;
using System.Data.Entity;
using Bank.DataAccess.Migrations;
using Bank.DataAccess.Repositories;
using Bank.Entities;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BankContext, Configuration>());
            //custom initializer (always drops and recreates)
            Database.SetInitializer(new BankDBInitializer());
            testClients();
            Console.ReadKey();
        }

        private static void testClients()
        {
            using (var context = new BankContext())
            {
                var repo = new ClientRepository(context);

                Console.WriteLine( "2 " + repo.getClientById(2).LastName );
                Console.WriteLine("");

                var address = new Address
                {
                    Id = 100,
                    Country = "Poland"
                };

                var client = new Client
                {
                    Id = 5,
                    FirstName = "Nowy",
                    LastName = "Klient",
                    AddressId = address.Id,
                    Address = address
                };

                repo.addNewClient(client);

                var clients = (List<Client>)repo.getClientList();
                foreach (var c in clients )
                {
                    Console.WriteLine( c.Id.ToString() + ' ' + c.FirstName + ' ' + c.LastName);
                }

                Console.WriteLine("");
                Console.WriteLine("End");
            }
        }

    }
}
