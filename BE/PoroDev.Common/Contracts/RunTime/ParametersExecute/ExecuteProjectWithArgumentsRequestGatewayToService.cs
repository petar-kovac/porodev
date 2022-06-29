using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.RunTime.ParametersExecute
{
    public  class ExecuteProjectWithArgumentsRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public Guid FileId { get; set; }
        
        public List<string>  Arguments{ get; set; }
    }
}
