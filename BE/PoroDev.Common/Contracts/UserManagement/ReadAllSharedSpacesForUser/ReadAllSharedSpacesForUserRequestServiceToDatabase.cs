﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.ReadAllSharedSpacesForUser
{
    public class ReadAllSharedSpacesForUserRequestServiceToDatabase
    {
        public Guid UserId { get; set; }
    }
}