using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts
{
    internal interface ICommunicationModel<T> where T : class, new()
    {
        T Entity { get; set; }
        Type ErrorType { get; set; }
       
    }
}
