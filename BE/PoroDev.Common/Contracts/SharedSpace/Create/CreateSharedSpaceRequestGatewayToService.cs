namespace PoroDev.Common.Contracts.SharedSpace.Create
{
    public class CreateSharedSpaceRequestGatewayToService
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }
    }
}