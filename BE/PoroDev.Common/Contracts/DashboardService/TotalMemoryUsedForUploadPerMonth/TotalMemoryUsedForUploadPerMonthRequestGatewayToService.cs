﻿namespace PoroDev.Common.Contracts.DashboardService.TotalMemoryUsedForUploadPerMonth
{
    public class TotalMemoryUsedForUploadPerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }

        public TotalMemoryUsedForUploadPerMonthRequestGatewayToService()
        {
        }

        public TotalMemoryUsedForUploadPerMonthRequestGatewayToService(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}