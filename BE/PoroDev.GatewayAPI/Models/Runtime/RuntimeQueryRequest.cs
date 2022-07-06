using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoroDev.GatewayAPI.Models.Runtime
{
    public class RuntimeQueryRequest
    {
        public Guid? FileId { get; set; }

        public DateTimeOffset? ExecutionStart { get; set; }

        public long? ExecutionTime { get; set; }

        public string? ExecutionOutput { get; set; }

        public bool? ExceptionHappened { get; set; }

        public List<string>? Arguments { get; set; }
    }
}
