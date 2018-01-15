using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using IOMail.Data;
using IOMail.Interfaces;

namespace IOMail
{
    public class SenderSmtp : ISender
    {
        private SmtpClient smtp;
        const string fromPassword = "ioproject";

        public SenderSmtp()// : this(new SmtpClient())
        {
            smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
        }

        public SenderSmtp(SmtpClient client)
        {
            smtp = client;
        }

        public void Send(IEmail email)
        {
            var message = CreateMessage(email);
            smtp.Credentials = new NetworkCredential(message.From.Address, fromPassword);

            smtp.Send(message);
            smtp?.Dispose();
        }

        private MailMessage CreateMessage(IEmail email)
        {
            var data = email.Data;
            var message = new MailMessage
            {
                Subject = data.Subject,
                Body = data.Body,
                From = new MailAddress(data.FromAddress.EmailAddress, "ioproject")
            };

            message.To.Add(new MailAddress(data.ToAddress.EmailAddress, "customer"));

            return message;
        }
    }
}
