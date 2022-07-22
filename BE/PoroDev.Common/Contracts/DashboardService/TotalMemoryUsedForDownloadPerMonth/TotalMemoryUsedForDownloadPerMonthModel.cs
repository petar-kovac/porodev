﻿namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForDownloadPerMonth
{
    public class TotalMemoryUsedForDownloadPerMonthModel
    {
        public string Month { get; set; }

        public double TotalMemoryUsedForDownloadInMBs { get; set; }

        public TotalMemoryUsedForDownloadPerMonthModel()
        {
        }

        public TotalMemoryUsedForDownloadPerMonthModel(double totalMemoryUsedForDownloadInMBs, string month)
        {
            TotalMemoryUsedForDownloadInMBs = totalMemoryUsedForDownloadInMBs;
            Month = month;
        }
    }
}