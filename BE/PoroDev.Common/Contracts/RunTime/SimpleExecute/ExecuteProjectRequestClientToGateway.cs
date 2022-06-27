using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.RunTime.SimpleExecute
{
    public class ExecuteProjectRequestClientToGateway
    {
        public string Jwt { get; set; }
        public string FileID { get; set; }
    }
}
