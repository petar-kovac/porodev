using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.UploadFile
{
    public class FileUploadRequestServiceToDatabase
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public Guid UserId { get; set; }

        public FileUploadRequestServiceToDatabase()
        {

        }

        public FileUploadRequestServiceToDatabase(IFormFile file, Guid userId)
        {
            using Stream stream = file.OpenReadStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            File = memoryStream.ToArray();
            FileName = file.FileName;

            // var content = file.OpenReadStream().
            //File = file;//izmjeniti na end point-u
            UserId = userId;

        }
    }
}
