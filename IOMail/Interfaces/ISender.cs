using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMail.Interfaces
{
    public interface ISender
    {
        void Send(IEmail email);
    }
}
