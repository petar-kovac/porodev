﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.UserManagement.SetMonthlyReportTime
{
    public class SetMonthlyReportTimeRequest
    {
        public int Day { get; set; }
        public int Hour { get; set; }
    }
}
