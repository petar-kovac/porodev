﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfDeletedFiles
{
    public class TotalNumberOFDeletedFilesRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalNumberOFDeletedFilesRequestServiceToDatabase()
        {

        }
        public TotalNumberOFDeletedFilesRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}