using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.RunTime.Query
{
    public class RuntimeQueryRequestGatewayToDatabase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid FileId { get; set; }

        public DateTimeOffset ExecutionStart { get; set; }

        public long ExecutionTime { get; set; }

        public string? ExecutionOutput { get; set; }

        public bool ExceptionHappened { get; set; }

        public string? Arguments { get; set; }

        public RuntimeQueryRequestGatewayToDatabase(Guid id, Guid userId, Guid fileId, DateTimeOffset executionStart, long executionTime, string? executionOutput, bool exceptionHappened, string? arguments)
        {
            Id = id;
            UserId = userId;
            FileId = fileId;
            ExecutionStart = executionStart;
            ExecutionTime = executionTime;
            ExecutionOutput = executionOutput;
            ExceptionHappened = exceptionHappened;
            Arguments = arguments;
        }
    }
}
