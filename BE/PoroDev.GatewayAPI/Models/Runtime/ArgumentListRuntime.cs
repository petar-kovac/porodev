namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class ArgumentListRuntime
    {
        public Guid ProjectId { get; set; }

        public List<string> Arguments { get; set; }
    }
}