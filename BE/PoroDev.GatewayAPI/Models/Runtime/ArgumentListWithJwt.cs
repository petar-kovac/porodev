using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ArgumentListWithUserId
    {
        public Guid UserId { get; set; }

        public Guid FileID { get; set; }

        public List<string> Arguments { get; set; }

        public ArgumentListWithUserId()
        {

        }
        public ArgumentListWithUserId(Guid userId, ArgumentListRuntime model)
        {
            UserId = userId;
            FileID = model.ProjectId;
            Arguments = model.Arguments;
        }
    }
}
