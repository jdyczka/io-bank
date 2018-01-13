using Bank.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories
{
    public interface IClientRepository
    {
        IEnumerable getClientList();
        Client getClientById(int id);
        Client getClientByEmail(string email);
        IEnumerable getClientAccounts(int id);
        void addNewClient(Client client);
        void updateClient(Client client);
    }
}
