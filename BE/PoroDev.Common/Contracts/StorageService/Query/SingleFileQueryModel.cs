using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.Query
{
    public class SingleFileQueryModel
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public DateTime UploadDateTime { get; set; }

        public ulong Length { get; set; }

        public string ContentType { get; set; }
    }
}
