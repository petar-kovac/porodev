namespace PoroDev.GatewayAPI.Models.StorageService
{
    public class FileExeReq
    {
        public string FileName { get; set; }

        public Guid? UserId { get; set; }

        public bool IsExe { get; set; }
    }
}
