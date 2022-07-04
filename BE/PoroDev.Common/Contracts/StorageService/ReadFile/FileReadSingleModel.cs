using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.ReadFile
{
    public class FileReadSingleModel
    {
        public string FileName { get; set; }

        public DateTime UploadTime { get; set; }

        public FileReadSingleModel()
        {

        }

        public FileReadSingleModel(string fileName, DateTime uploadTime)
        {
            FileName = fileName;
            UploadTime = uploadTime;
        }
    }
}
