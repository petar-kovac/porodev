namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestServiceToDatabase
    {
        public Guid SharedSpaceID { get; set; }
        public Guid UserToAddId { get; set; }
    }
}