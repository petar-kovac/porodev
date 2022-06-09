using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Create
{
    public class UserCreateResponseDatabaseToService 
    {
        public DataUserModel Entity { get; set; }
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
