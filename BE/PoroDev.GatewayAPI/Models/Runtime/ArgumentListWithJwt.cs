using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ArgumentListWithJwt
    {
        public string Jwt { get; set; }

        public Guid FileID { get; set; }

        public List<string> Arguments { get; set; }

        public ArgumentListWithJwt()
        {

        }
        public ArgumentListWithJwt(string jwt, ArgumentListRuntime model)
        {
            Jwt = jwt;
            FileID = model.ProjectId;
            Arguments = model.Arguments;
        }
    }
}
