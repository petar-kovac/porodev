using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common
{
    public interface IDownloadRequest
    {
        public MessageData<byte[]> File { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
