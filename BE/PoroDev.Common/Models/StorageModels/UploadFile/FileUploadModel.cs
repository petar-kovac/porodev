using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.StorageModels.UploadFile
{
    public class FileUploadModel
    {
        public byte[] File { get; set; }

        public Guid UserId { get; set; }

        public FileUploadModel()
        {

        }

        public FileUploadModel(IFormFile file, Guid userId)
        {
            using Stream stream = file.OpenReadStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            File = memoryStream.ToArray();

            // var content = file.OpenReadStream().
            //File = file;//izmjeniti na end point-u
            UserId = userId;

        }
    }
}
