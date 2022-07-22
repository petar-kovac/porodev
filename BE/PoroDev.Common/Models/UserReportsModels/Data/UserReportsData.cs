using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.UserReportsModels.Data
{
    public class UserReportsData
    {
        public Guid Id { get; set; }
        public ulong FileDownloadTotal { get; set; }

        public ulong FileUploadTotal { get; set; }

        public ushort RuntimeTotal { get; set; }

        public string Month { get; set; }

        public Guid CurrentUserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel CurrentUser { get; set; }

        public UserReportsData()
        {

        }

        public UserReportsData(Guid id, ulong fileDownloadTotal, ulong fileUploadTotal, ushort runtimeTotal, string month, Guid currentUserId)
        {
            Id = id;
            FileDownloadTotal = fileDownloadTotal;
            FileUploadTotal = fileUploadTotal;
            RuntimeTotal = runtimeTotal;
            Month = month;
            CurrentUserId = currentUserId;
        }
    }
}
