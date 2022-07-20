namespace PoroDev.GatewayAPI.Models.SharedSpace
{
    public class AddFileToSharedSpaceRequest
    {
        public Guid SharedSpaceId { get; set; }

        public string FileId { get; set; }
    }
}
