﻿using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.Login
{
    public class LoginResponseDatabaseToService : ICommunicationModel<DataUserModel>
    {
        public DataUserModel Entity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ErrorName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
