﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimePerMonth
{
    public class TotalRunTimePerMonthRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public int NumberOfMonthsToShow { get; set; }


        public TotalRunTimePerMonthRequestGatewayToService()
        {

        }

        public TotalRunTimePerMonthRequestGatewayToService(Guid userId, int numberOfMonthsToShow)
        {
            UserId = userId;
            NumberOfMonthsToShow = numberOfMonthsToShow;
        }
    }
}
