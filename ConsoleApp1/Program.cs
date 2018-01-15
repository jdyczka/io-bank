using System;
using System.Linq;
using Bank.DataAccess;
using System.Data.Entity;
using Bank.DataAccess.Migrations;
using Bank.Entities;
using System.Collections.Generic;
using Bank.DataAccess.Repositories;
using IOMail;

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
            testClients();
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
            Console.ReadKey();
        }

        private static void testClients()
        {
            using (var context = new BankContext())
            {
                var clientRepo = new ClientRepository(context);
                var accountRepo = new AccountRepository(context);

                Console.WriteLine("4 " + clientRepo.getClientById(4).LastName);
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
                foreach (var c in clients)
                {
                    Console.WriteLine(c.Id.ToString() + ' ' + c.FirstName + ' ' + c.LastName);
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
