using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;
using System.Data.Entity;

namespace Bank.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private BankContext _context;

        public ClientRepository(BankContext context)
        {
            _context = context;
        }

        public void addNewClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public IEnumerable getClientAccounts(int clientId)
        {
            return getClientById(clientId).Accounts.ToList();
        }

        public Client getClientByEmail(string email)
        {
            return _context.Clients.Where(c => c.Email == email).FirstOrDefault();
        }

        public Client getClientById(int id)
        {
            return _context.Clients.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable getClientList()
        {
            return _context.Clients.OrderBy(c => c.LastName).ToList();
        }

        public void updateClient(Client client)
        {
            _context.Clients.Attach( client );
            _context.Entry(client).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
