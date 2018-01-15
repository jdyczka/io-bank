using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories
{
    public class AccountRepository
    {
        private BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public IEnumerable getAccountList()
        {
            return _context.Accounts.ToList();
        }
    }
}
