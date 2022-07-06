﻿using PoroDev.Common.Models.UserModels.Data;
using System.Text.Json.Serialization;

namespace PoroDev.Common.Models.RuntimeModels.Data
{
    public class RuntimeData
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FileId { get; set; }

        public DateTimeOffset ExecutionStart { get; set; }

        public long ExecutionTime { get; set; }

        public string ExecutionOutput { get; set; }

        public bool ExceptionHappened { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel User { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Arguments { get; set; }

        public RuntimeData()
        {
        }

        public RuntimeData(Guid userId, string fileId, DateTimeOffset executionStart, long executionTime, string executionOutput)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FileId = fileId;
            ExecutionStart = executionStart;
            ExecutionTime = executionOutput == String.Empty ? 0 : executionTime;
            ExecutionOutput = executionOutput;
            ExceptionHappened = executionOutput == "";
        }

        public RuntimeData(Guid userId, string fileId, DateTimeOffset executionStart, long executionTime, string executionOutput, string arguments) : this(userId, fileId, executionStart, executionTime, executionOutput)
        {
            Arguments = arguments;
        }
    }
}