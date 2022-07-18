namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ExecuteProjectRequestClientToGateway
    {
        public string Jwt { get; set; }
        public Guid FileID { get; set; }

        public ExecuteProjectRequestClientToGateway()
        {
        }

        public ExecuteProjectRequestClientToGateway(string jwt, Guid fileID)
        {
            Jwt = jwt;
            FileID = fileID;
        }
    }
}