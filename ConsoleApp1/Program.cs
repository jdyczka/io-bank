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
            testAddresses();
            testAccounts();
            testEmployees();

            Console.WriteLine("");
            Console.WriteLine("End");
            Console.ReadKey();
        }

        private static void testClients()
        {
            using (var context = new BankContext())
            {
                var clientRepo = new ClientRepository(context);
                var accountRepo = new AccountRepository(context);

                // GET CLIENT BY ID
                Console.WriteLine( "4 " + clientRepo.getClientById(4).LastName );
                Console.WriteLine("");

                // ADD CLIENT
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

                // GET ALL CLIENTS
                var clients = (List<Client>)clientRepo.getClientList();
                foreach (var c in clients )
                {
                    Console.WriteLine( c );
                }

                Console.WriteLine("");

                // UPDATE CLIENT
                client.LastName = "Klient-Zmieniony";
                clientRepo.updateClient(client);
                Console.WriteLine( client.Id.ToString() + ' ' + clientRepo.getClientById(client.Id).FirstName + ' ' + clientRepo.getClientById(client.Id).LastName);
                Console.WriteLine("");
            }
        }

        private static void testEmployees()
        {
            using (var context = new BankContext())
            {
                var employeeRepo = new EmployeeRepository(context);
                
                Console.WriteLine("3 " + employeeRepo.getEmployeeById(3).LastName);
                Console.WriteLine("");
            }
        }

        private static void testAddresses()
        {
            using (var context = new BankContext())
            {
                var addressRepo = new AddressRepository(context);
                
                var addresses = (List<Address>)addressRepo.getAddressList();
                foreach (var a in addresses)
                {
                    Console.WriteLine(a);
                }
                Console.WriteLine("");
            }
        }

        private static void testAccounts()
        {
            using (var context = new BankContext())
            {
                var accountRepo = new AccountRepository(context);

                var accounts = (List<Account>)accountRepo.getAccountList();
                foreach (var a in accounts)
                {
                    Console.WriteLine(a.AccountNo);
                }
                Console.WriteLine("");
            }
        }

    }
}
