namespace PoroDev.GatewayAPI.Models.SharedSpace
{
    public class DeletFileRequest
    {
        public Guid SharedSpaceId { get; set; }

        public string FileId { get; set; }
    }
}