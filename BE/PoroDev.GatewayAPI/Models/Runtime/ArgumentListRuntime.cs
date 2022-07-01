using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ArgumentListRuntime
    {
        public Guid ProjectId { get; set; }

        public List<string> Arguments { get; set; }
    }
}
