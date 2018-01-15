using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMail.Interfaces
{
    public interface ITemplateParser
    {
        string Parse(string templatename, dynamic data);
    }
}
