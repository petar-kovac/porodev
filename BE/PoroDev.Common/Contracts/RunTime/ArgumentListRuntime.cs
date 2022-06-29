using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.RunTime
{
    public class ArgumentListRuntime
    {
        public Guid ProjectId { get; set; }

        public List<string> Arguments { get; set; }
    }
}
