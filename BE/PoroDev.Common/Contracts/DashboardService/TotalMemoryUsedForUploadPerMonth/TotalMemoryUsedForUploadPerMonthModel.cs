﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthModel
    {
        public string Month { get; set; }

        public double TotalMemoryUsedForUploadInMBs { get; set; }

        public TotalMemoryUsedForUploadPerMonthModel()
        {

        }

        public TotalMemoryUsedForUploadPerMonthModel(string month, double totalMemoryUsedForUploadInMBs)
        {
            Month = month;
            TotalMemoryUsedForUploadInMBs = totalMemoryUsedForUploadInMBs;
        }
    }
}
