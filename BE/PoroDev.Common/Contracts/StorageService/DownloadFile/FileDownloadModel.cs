using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.DownloadFile
{
    public class FileDownloadModel
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public FileDownloadModel()
        {

        }

        public FileDownloadModel(string fileName, byte[] file)
        {
            FileName = fileName;
            File = file;
        }
    }
}
