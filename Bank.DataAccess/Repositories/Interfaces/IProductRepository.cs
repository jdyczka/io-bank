using Bank.Entities.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable getAccountList();
        void openAccount( int clientId, 
            string accountName, Currency currency, AccountType accountType, double interestRate );
        void closeAccount( string accountNo );

        IEnumerable getLoanList();
        void openLoan(int clientId, 
            string accountName, Currency currency, AccountType accountType, double interestRate,
            Decimal amount, Decimal inhstallmentAmount, int installmentFrequency, DateTime nextDueDate);
        void closeLoan(string accountNo);

        IEnumerable getTimeDepositList();
        void openTimeDeposit(int clientId, 
            string accountName, Currency currency, AccountType accountType, double interestRate,
            DateTime expirationDate);
        void closeTimeDeposit(string accountNo);
    }
}
