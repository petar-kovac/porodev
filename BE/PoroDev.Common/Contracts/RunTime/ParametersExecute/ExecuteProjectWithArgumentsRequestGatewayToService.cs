namespace PoroDev.Common.Contracts.RunTime.ParametersExecute
{
    public class ExecuteProjectWithArgumentsRequestGatewayToService
    {
        public Guid UserId { get; set; }

        public Guid FileId { get; set; }

        public List<String> Arguments { get; set; }

        public ExecuteProjectWithArgumentsRequestGatewayToService()
        {
        }

        public ExecuteProjectWithArgumentsRequestGatewayToService(Guid fileId, Guid userId, List<string> arguments)
        {
            UserId = userId;
            FileId = fileId;
            Arguments = arguments;
        }
    }
}