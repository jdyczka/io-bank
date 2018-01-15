using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Dynamic;
using IOMail.Data;
using IOMail.Interfaces;

namespace IOMail
{
    public class Email : IEmail
    {
        public EmailData Data { get; set; }
        public ITemplateParser Parser { get; set; }
        public ISender Sender { get; set; }

        public Email()
            : this(new Parser(), new SenderSmtp())
        {

        }

        public Email(ITemplateParser parser, ISender sender)
            : this(parser, sender, null)
        {

        }

        public Email(string emailAddress)
            : this(new Parser(), new SenderSmtp(), emailAddress)
        {

        }

        public Email(ITemplateParser parser, ISender sender, string emailAddress)
        {
            Data = new EmailData()
            {
                FromAddress = new Address() { EmailAddress = emailAddress }
            };
            Parser = parser;
            Sender = sender;
        }

        public IEmail To(string emailAddress)
        {
            Data.ToAddress = new Address(emailAddress);

            return this;
        }

        public static IEmail From(string emailAddress)
        {
            var email = new Email
            {
                Data = { FromAddress = new Address(emailAddress) }
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
            // TODO: Add implementation
            //Data.Body += data.GetType().GetProperty("Name").GetValue(data, null);
            string result = Parser.Parse(templatename, data);
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
