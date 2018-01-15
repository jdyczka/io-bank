using Bank.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;
using System.Collections;

namespace Bank.DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private BankContext _context;

        public AddressRepository(BankContext context)
        {
            _context = context;
        }

        public void addNewAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public Address getAddressById(int id)
        {
            return _context.Addresses.Where( a => a.Id == id ).FirstOrDefault();
        }

        public IEnumerable getAddressList()
        {
            return _context.Addresses.ToList();
        }
    }
}
