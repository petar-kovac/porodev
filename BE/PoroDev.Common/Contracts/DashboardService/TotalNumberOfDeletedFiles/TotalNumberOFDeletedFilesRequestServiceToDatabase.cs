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