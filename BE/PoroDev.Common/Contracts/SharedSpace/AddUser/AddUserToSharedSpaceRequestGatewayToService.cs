namespace PoroDev.Common.Contracts.SharedSpace.AddUser
{
    public class AddUserToSharedSpaceRequestGatewayToService
    {
        public Guid SharedSpaceID { get; set; }
        public Guid UserId { get; set; }

        public AddUserToSharedSpaceRequestGatewayToService()
        {

        }
        public AddUserToSharedSpaceRequestGatewayToService(Guid sharedSpaceID, Guid userToAddId)
        {
            SharedSpaceID = sharedSpaceID;
            UserId = userToAddId;
        }
    }
}