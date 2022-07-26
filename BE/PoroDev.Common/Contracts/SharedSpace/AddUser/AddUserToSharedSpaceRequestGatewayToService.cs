namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestGatewayToService
    {
        public Guid SharedSpaceID { get; set; }
        public Guid UserToAddId { get; set; }
    }
}