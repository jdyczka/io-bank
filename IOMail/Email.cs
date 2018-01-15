using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Dynamic;
using IOMail.Data;
using IOMail.Interfaces;
using System.IO;
using Bank.Entities;
using Bank.DataAccess.Repositories;
using Bank.DataAccess;

namespace IOMail
{
    public class Email : IEmail
    {
        public EmailData Data { get; set; }
        public ITemplateParser Parser { get; set; }
        public ISender Sender { get; set; }
        public BankContext Context { get; set; }
        private IClientRepository ClientRepository;

        public Email(BankContext context)
            : this(new Parser(), new SenderSmtp(), new ClientRepository(context))
        {

        }

        public Email(ITemplateParser parser, ISender sender, IClientRepository clientRepository)
            : this(parser, sender, clientRepository, null)
        {

        }

        public Email(string emailAddress, BankContext context)
            : this(new Parser(), new SenderSmtp(), new ClientRepository(context), emailAddress)
        {

        }

        public Email(ITemplateParser parser, ISender sender, IClientRepository clientRepository, string emailAddress)
        {
            Data = new EmailData()
            {
                FromAddress = new Data.Address() { EmailAddress = emailAddress }
            };
            Parser = parser;
            Sender = sender;
            ClientRepository = clientRepository;
        }

        public IEmail To(int id)
        {
            Client client = ClientRepository.getClientById(id);
            string emailAddress = client.Email;
            Data.ToAddress = new Data.Address(emailAddress);

            return this;
        }

        public static IEmail From(BankContext context, string emailAddress)
        {
            var email = new Email(context)
            {
                Data = { FromAddress = new Data.Address(emailAddress) }
            };

            return email;
        }

        public IEmail Subject(string subject)
        {
            Data.Subject = subject;
            return this;
        }

        public IEmail Body(string body)
        {
            Data.Body = body;
            return this;
        }

        public IEmail UseTemplate(string templatename, dynamic data)
        {
            string template = "Blad! Nie znaleziono wzorco!";
            foreach (string line in System.IO.File.ReadAllLines(@"szablony.txt"))
            {
                string[] splits = line.Split(':');
                splits[0] = splits[0].Substring(0, splits[0].Count() - 1);
                splits[1] = splits[1].Substring(1, splits[1].Count() - 1);
                if (splits[0] == templatename)
                {
                    template = splits[1];
                    break;
                }
            }
            
            string result = Parser.Parse(template, data);
            Data.Body = result;

            return this;
        }

        public IEmail Send()
        {
            Sender.Send(this);
            return this;
        }
    }
}
