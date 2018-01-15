using Bank.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities.Enums;
using Bank.Entities;

namespace Bank.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private BankContext _context;

        public ProductRepository(BankContext context)
        {
            _context = context;
        }

        public void closeAccount(string accountNo)
        {
            throw new NotImplementedException();
        }

        public void closeLoan(string accountNo)
        {
            throw new NotImplementedException();
        }

        public void closeTimeDeposit(string accountNo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable getAccountList()
        {
            return _context.Accounts.ToList();
        }

        public IEnumerable getLoanList()
        {
            return _context.Loans.ToList();
        }

        public IEnumerable getTimeDepositList()
        {
            return _context.TimeDeposits.ToList();
        }

        public void openAccount(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate)
        {
            var account = new Account(accountName, currency, accountType, interestRate);
            _context.Accounts.Add(account);
            _context.Clients.Where(c => c.Id == clientId).FirstOrDefault().Accounts.Add(account);
            _context.SaveChanges();
        }

        public void openLoan(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate, decimal amount, decimal inhstallmentAmount, int installmentFrequency, DateTime nextDueDate)
        {
            throw new NotImplementedException();
        }

        public void openTimeDeposit(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate, DateTime expirationDate)
        {
            throw new NotImplementedException();
        }
    }
}
