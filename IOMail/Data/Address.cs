using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMail.Data
{
    public class Address
    {
        public string EmailAddress { get; set; }

        public Address()
        {
        }

        public Address(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
    }
}
