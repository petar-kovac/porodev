using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ExecuteProjectRequestClientToGateway
    {
        public string Jwt { get; set; }
        public Guid FileID { get; set; }

        public ExecuteProjectRequestClientToGateway()
        {

        }
        public ExecuteProjectRequestClientToGateway(string jwt, Guid fileID)
        {
            Jwt = jwt;
            FileID = fileID;
        }
    }
}
