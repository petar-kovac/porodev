using PoroDev.Common.Contracts.Create;
using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserModels.Create
{
    public class UserCreateResponseDatabaseToService : IUserCreateResponseDatabaseToService
    {
        public DataUserModel Entity { get; set ; }
        public string ErrorName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
