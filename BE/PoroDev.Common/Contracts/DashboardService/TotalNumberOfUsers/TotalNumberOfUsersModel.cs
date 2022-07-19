﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersModel
    {
        public int _numberOfUsers { get; set; }

        public TotalNumberOfUsersModel()
        {

        }

        public TotalNumberOfUsersModel(int numberOfUsers)
        {
            _numberOfUsers = numberOfUsers;
        }
    }
}