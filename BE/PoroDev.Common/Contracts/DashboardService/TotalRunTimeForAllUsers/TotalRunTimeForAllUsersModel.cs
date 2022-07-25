namespace PoroDev.Common.Contracts.DashboardService.TotalRunTimeForAllUsers
{
    public class TotalRunTimeForAllUsersModel
    {
        public int NumberOfTotalRunTimeForAllUsers { get; set; }

        public TotalRunTimeForAllUsersModel()
        {
        }

        public TotalRunTimeForAllUsersModel(int numberOfTotalRunTimeForAllUsers)
        {
            NumberOfTotalRunTimeForAllUsers = numberOfTotalRunTimeForAllUsers;
        }
    }
}