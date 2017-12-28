using System.Data.Entity;
using Bank.Entities;

namespace Bank.DataAccess
{
    public class BankContext : DbContext
    {
        //public BankContext() { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TimeDeposit> TimeDeposits { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
