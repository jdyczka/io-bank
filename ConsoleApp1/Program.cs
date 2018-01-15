using System;
using System.Linq;
using Bank.DataAccess;
using System.Data.Entity;
using Bank.DataAccess.Migrations;
using Bank.Entities;
using System.Collections.Generic;
using Bank.DataAccess.Repositories;
using IOMail;
using Bank.Entities.Enums;

namespace ConsoleApp1
{
    class Program
    {
        const string ourAddress = "ioproject2017pl@gmail.com";

        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BankContext, Configuration>());
            //custom initializer (always drops and recreates)
            Database.SetInitializer(new BankDBInitializer());
            testProducts();
            testClients();
            testAddresses();
            testEmployees();

            Console.WriteLine("Press key to test sending email by template");
            Console.ReadKey();
            Client client;
            var context = new BankContext();
            var clientRepo = new ClientRepository(context);
            client = clientRepo.getClientById(4);

            //EMAIL WPISANY POPRZEZ WPISANIE NAZWY WZORCA ZAWARTEGO W PLIKU CONSOLEAPP/BIN/DEBUG/SZABLONY.TXT
            testSendingEmailByTemplate(client, context, "Naglowek wysylanego maila ze wzorca", "usercreate");

            Console.WriteLine("Press key to test sending email manually");
            Console.ReadKey();

            //EMAIL WYSYLANY POPRZEZ WPISANIE TEKSTU WIADOMOŚCI
            testSendingEmailWrittenManually(client, context, "Naglowek wysylanego manualnie maila", "Elo elo, dotarlem do celu");
            
            Console.WriteLine("");
            Console.WriteLine("End");
            Console.ReadKey();
        }

        private static void testClients()
        {
            using (var context = new BankContext())
            {
                var clientRepo = new ClientRepository(context);
                var accountRepo = new ProductRepository(context);

                // GET CLIENT BY ID
                var someClient = clientRepo.getClientById(4);
                Console.WriteLine( "4 " + someClient.LastName );
                Console.WriteLine( someClient.GetSearchTags() );
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
                Console.WriteLine("Liczba klientów: " + clients.Count);
                Console.WriteLine("");
                foreach (var c in clients)
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

                var employees = (List<Employee>)employeeRepo.getEmployeeList();
                Console.WriteLine("Liczba pracowników: " + employees.Count);
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

        private static void testProducts()
        {
            using (var context = new BankContext())
            {
                var prodRepo = new ProductRepository(context);

                prodRepo.openAccount(1, "konto", Currency.PLN, AccountType.Regular, 0.01);

                var accounts = (List<Account>)prodRepo.getAccountList();
                foreach (var a in accounts)
                {
                    Console.WriteLine(a.AccountNo);
                }
                Console.WriteLine("");
            }
        }

        private static void testSendingEmailByTemplate(Client client, BankContext context, string subject, string templateName)
        {
            int clientId = client.Id;

            var email = Email.From(context, ourAddress) // tutaj podajemy naszego maila - moze być na sztywno
                .To(clientId)     // Tutaj podajemy id clienta do którego wysyłamy maila
                .Subject(subject) // Tutaj podajemy tytul maila
                                  //.Body("Library Test Body")
                .UseTemplate(templateName, new { Name = client.FirstName, Surname = client.LastName }) // W pliku ConsoleApp/bin/Debug/szablony.txt są zawarte szablony maili
                .Send();

            Console.WriteLine(email.Data.Body);
        }

        private static void testSendingEmailWrittenManually(Client client, BankContext context, string subject, string text)
        {
            int clientId = client.Id;

            var email = Email.From(context, ourAddress)
                .To(clientId)
                .Subject(subject)
                .Body(text) // Tutaj podajemy tresc wysylanego maila
                            //.UseTemplate(templateName, new { Name = client.FirstName, Surname = client.LastName })
                .Send();

            Console.WriteLine(email.Data.Body);
        }

    }
}
