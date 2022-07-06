namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadRequestServiceToDatabase
    {
        public Guid UserId { get; set; }

        public FileReadRequestServiceToDatabase()
        {
        }

        public FileReadRequestServiceToDatabase(Guid userId)
        {
            UserId = userId;
        }
    }
}