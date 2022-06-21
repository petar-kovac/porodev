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
        public IFormFile File;

        public Guid UserId;

        public FileUploadModel(IFormFile file, Guid userId)
        {
            File = file;//izmjeniti na end point-u
            UserId = userId;

        }
    }
}
