﻿using System;
using System.Linq;
using Bank.DataAccess;
using System.Data.Entity;
using Bank.DataAccess.Migrations;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BankContext, Configuration>());
            //custom initializer (always drops and recreates)
            Database.SetInitializer(new BankDBInitializer());
            GetClients();
        }

        private static void GetClients()
        {
            using (var context = new BankContext())
            {

                var clients = context.Clients.ToList();
                foreach (var client in clients)
                {
                    Console.WriteLine(client.FirstName);
                }

                Console.WriteLine("");
                Console.WriteLine("End");
            }
        }

    }
}
