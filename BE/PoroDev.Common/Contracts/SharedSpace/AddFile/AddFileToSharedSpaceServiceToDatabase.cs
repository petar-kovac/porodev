namespace PoroDev.Common.Contracts.SharedSpace.AddFile
{
    public class AddFileToSharedSpaceServiceToDatabase
    {
        public Guid SharedSpaceId { get; set; }

        public string FileId { get; set; }

        public Guid UserId { get; set; }

        public AddFileToSharedSpaceServiceToDatabase()
        {
        }

        public AddFileToSharedSpaceServiceToDatabase(Guid sharedSpaceId, string fileId, Guid userId)
        {
            SharedSpaceId = sharedSpaceId;
            FileId = fileId;
            UserId = userId;
        }
    }
}