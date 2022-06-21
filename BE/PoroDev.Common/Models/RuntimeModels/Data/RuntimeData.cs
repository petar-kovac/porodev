using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.RuntimeModels.Data
{
    public class RuntimeData
    {
        public Guid UserId { get; set; }

        public Guid FileId { get; set; }

        public DateTimeOffset ExecutionStart { get; set; }

        public long ExecutionTime { get; set; }

        public string ExecutionOutput { get; set; }

        public bool ExceptionHappened { get; set; }

        public DataUserModel User { get; set; }



    }
}
