﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUsersRequestServiceToDatabase()
        {

        }

        public TotalNumberOfUsersRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}