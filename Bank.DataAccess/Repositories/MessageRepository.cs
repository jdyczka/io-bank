using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.Entities;

namespace Bank.DataAccess.Repositories
{
    class MessageRepository : IMessageRepository
    {
        private BankContext _context;

        public MessageRepository(BankContext context)
        {
            _context = context;
        }

        public void addNewMessage(string text)
        {
            _context.Messages.Add(new Message(text));
            _context.SaveChanges();
        }

        public IEnumerable getAllMessages()
        {
            return _context.Messages.ToList();
        }

        public Message getMessageById(int id)
        {
            return _context.Messages.Where(m => m.Id == id).FirstOrDefault();
        }
    }
}
