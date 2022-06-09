using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts
{
    public interface ICommunicationModel<T> where T : class, new()
    {
        T Entity { get; set; }

        string ErrorName { get; set; }

        string ErrorMessage { get; set; }
       
    }
}
