using Bank.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Linq;
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
            closeAccount(accountNo);
        }

        public void closeTimeDeposit(string accountNo)
        {
            closeAccount(accountNo);
        }

        public Account getAccountByNo(string accountNo)
        {
            return _context.Accounts.Where(a => a.AccountNo == accountNo).FirstOrDefault();
        }

        public IEnumerable getAccountList()
        {
            return _context.Accounts.ToList();
        }

        public Loan getLoanByNo(string accountNo)
        {
            return _context.Loans.Where(l => l.Account.AccountNo == accountNo).FirstOrDefault();
        }

        public IEnumerable getLoanList()
        {
            return _context.Loans.ToList();
        }

        public TimeDeposit getTimeDepositByNo(string accountNo)
        {
            return _context.TimeDeposits.Where(t => t.Account.AccountNo == accountNo).FirstOrDefault();
        }

        public IEnumerable getTimeDepositList()
        {
            return _context.TimeDeposits.ToList();
        }

        public string openAccount(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate)
        {
            var account = new Account(accountName, currency, accountType, interestRate);
            _context.Accounts.Add(account);
            _context.Clients.Where(c => c.Id == clientId).FirstOrDefault().Accounts.Add(account);
            _context.SaveChanges();
            return account.AccountNo;
        }

        public string openLoan(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate, 
            decimal amount, decimal installmentAmount, int installmentFrequency, DateTime nextDueDate)
        {
            var accountNo = openAccount(clientId, accountName, currency, accountType, interestRate);
            _context.Loans.Add(new Loan { AccountNo = accountNo, Amount = amount, InstallmentAmount = installmentAmount, InstallmentFrequency = installmentFrequency, NextDueDate = nextDueDate });
            _context.SaveChanges();
            return accountNo;
        }

        public string openTimeDeposit(int clientId, string accountName, Currency currency, AccountType accountType, double interestRate, DateTime expirationDate)
        {
            var accountNo = openAccount(clientId, accountName, currency, accountType, interestRate);
            _context.TimeDeposits.Add(new TimeDeposit { AccountNo = accountNo, ExpirationDate = expirationDate });
            _context.SaveChanges();
            return accountNo;
        }
    }
}
