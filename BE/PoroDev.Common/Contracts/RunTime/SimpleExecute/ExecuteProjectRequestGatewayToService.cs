namespace PoroDev.Common.Contracts.RunTime.SimpleExecute
{
    public class ExecuteProjectRequestGatewayToService
    {
        public Guid UserId { get; set; }
        public string FileID { get; set; }

        public ExecuteProjectRequestGatewayToService()
        {
        }

        public ExecuteProjectRequestGatewayToService(Guid userId, string fileID)
        {
            UserId = userId;
            FileID = fileID;
        }
    }
}