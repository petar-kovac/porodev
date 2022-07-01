using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.ReadById
{
    public class UserReadByIdRequestGatewayToService
    {
        public Guid Id { get; set; }

        public UserReadByIdRequestGatewayToService()
        {

        }

        public UserReadByIdRequestGatewayToService(Guid id)
        {
            Id = id;
        }
    }
}
