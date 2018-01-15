using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOMail.Data;

namespace IOMail.Interfaces
{
    public interface IEmail
    {
        EmailData Data { get; set; }
        ITemplateParser Parser { get; set; }
        ISender Sender { get; set; }

        IEmail To(string emailAddress);
        //IEmail From(string emailAddress);
        IEmail Subject(string subject);
        IEmail Body(string body);
        IEmail UseTemplate(string templatename, dynamic data);
        IEmail Send();
    }
}
