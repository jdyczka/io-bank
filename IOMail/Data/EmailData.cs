using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMail.Data
{
    public class EmailData
    {
        public Address ToAddress { get; set; }
        public Address FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailData()
        {
            //ToAddresses = new Address();
        }
    }
}
