using System;
using System.Linq;
using Bank.DataAccess;
using System.Data.Entity;
using Bank.DataAccess.Migrations;
using Bank.Entities;
using System.Collections.Generic;
using Bank.DataAccess.Repositories;

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
                var clientRepo = new ClientRepository(context);
                var accountRepo = new AccountRepository(context);

                Console.WriteLine( "4 " + clientRepo.getClientById(4).LastName );
                Console.WriteLine("");

                var address = new Address
                {
                    Country = "Poland"
                };

                var client = new Client
                {
                    FirstName = "Nowy",
                    LastName = "Klient",
                    AddressId = address.Id,
                    Address = address
                };

                clientRepo.addNewClient(client);

                var clients = (List<Client>)clientRepo.getClientList();
                foreach (var c in clients )
                {
                    Console.WriteLine( c.Id.ToString() + ' ' + c.FirstName + ' ' + c.LastName);
                }

                Console.WriteLine("");

                var accounts = (List<Account>)accountRepo.getAccountList();
                foreach (var a in accounts)
                {
                    Console.WriteLine(a.AccountNo);
                }

                Console.WriteLine("");
                Console.WriteLine("End");
            }
        }

    }
}
