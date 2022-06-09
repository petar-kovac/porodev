using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Create
{
    public class UserCreateResponseServiceToGateway
    {
        public DataUserModel Entity { get; set; }
        public string ExceptionName { get; set; }
        public string HumanReadableExceptionMessage { get; set; }
    }
}
