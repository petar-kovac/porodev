using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Login
{
    internal class LoginResponseDatabaseToService : ICommunicationModel<DataUserModel>
    {
        public DataUserModel Entity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Type ErrorType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
