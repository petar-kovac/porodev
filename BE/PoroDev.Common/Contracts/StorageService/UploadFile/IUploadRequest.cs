using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public interface IUploadRequest
    {
        public string FileName { get; set; }

        public MessageData<byte[]> File { get; set; }

        public string ContentType { get; set; }

        public Guid UserId { get; set; }
    }
}
