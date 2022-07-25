using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.Query
{
    public class FileQueryGatewayToService
    {
        public Guid UserId { get; set; }

        public string? FileId { get; set; }

        public string? FileName { get; set; }

        public DateTime? UploadTime { get; set; }

        public ulong? Size { get; set; }

        public string? ContentType { get; set; }

        public bool? IsExe { get; set; }

        public FileQueryGatewayToService()
        {

        }

        public FileQueryGatewayToService(Guid userId, string? fileId = null, string? fileName = null, DateTime? uploadTime = null, ulong? size = null, string? contentType = null, bool? isExe = null)
        {
            UserId = userId;
            FileId = fileId;
            FileName = fileName;
            UploadTime = uploadTime;
            Size = size;
            ContentType = contentType;
            IsExe = isExe;
        }
    }
}
