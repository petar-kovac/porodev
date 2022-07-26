namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestServiceToDatabase
    {
        public Guid SharedSpaceId { get; set; }
        public Guid UserId { get; set; }
    }
}