using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUsers
{
    public class TotalNumberOfUsersModel
    {
        public int NumberOfUsers { get; set; }

        public TotalNumberOfUsersModel()
        {

        }

        public TotalNumberOfUsersModel(int numberOfUsers)
        {
            NumberOfUsers = numberOfUsers;
        }
    }
}
