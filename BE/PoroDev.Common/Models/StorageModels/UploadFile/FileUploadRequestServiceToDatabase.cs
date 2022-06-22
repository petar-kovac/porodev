using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.StorageModels.UploadFile
{
    public class FileUploadRequestServiceToDatabase
    {
        public IFormFile File { get; set; }

        public Guid UserId { get; set; }
    }
}
