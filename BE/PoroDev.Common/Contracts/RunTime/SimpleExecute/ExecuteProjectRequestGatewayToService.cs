using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.RunTime.SimpleExecute
{
    public class ExecuteProjectRequestGatewayToService
    {
        public Guid UserId { get; set; }
        public Guid FileID { get; set; }

        public ExecuteProjectRequestGatewayToService()
        {

        }
        public ExecuteProjectRequestGatewayToService(Guid userId, Guid fileID)
        {
            UserId = userId;
            FileID = fileID;
        }
    }
}
