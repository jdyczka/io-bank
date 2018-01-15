using Bank.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Repositories
{
    public interface IMessageRepository
    {
        Message getMessageById(int id);
        IEnumerable getAllMessages();
        void addNewMessage(string text);
    }
}
