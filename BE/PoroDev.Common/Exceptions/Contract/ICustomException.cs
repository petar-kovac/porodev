using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Exceptions.Contract
{
    public interface ICustomException
    {
        public string HumanReadableErrorMessage { get; set; }
    }
}
