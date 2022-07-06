namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public FileReadRequestGatewayToService()
        {
        }

        public FileReadRequestGatewayToService(Guid userId)
        {
            UserId = userId;
        }
    }
}