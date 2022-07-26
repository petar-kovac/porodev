namespace PoroDev.Common.Contracts.DashboardService.TotalNumberOfUploadedFiles
{
    public class TotalNumberOfUploadedFilesRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public TotalNumberOfUploadedFilesRequestServiceToDatabase()
        {
        }

        public TotalNumberOfUploadedFilesRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}