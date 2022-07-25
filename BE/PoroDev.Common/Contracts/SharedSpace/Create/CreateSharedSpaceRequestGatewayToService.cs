using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.SharedSpace.Create
{
    public class CreateSharedSpaceRequestGatewayToService
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}
