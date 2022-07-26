namespace PoroDev.Common.Contracts.SharedSpace.Create
{
    public class CreateSharedSpaceRequestServiceToDatabase
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}