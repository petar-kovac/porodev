﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.ReadByIdWithRuntime
{
    public class UserReadByIdWithRuntimeRequestGatewayToService
    {
        public Guid Id { get; set; }
    }
}