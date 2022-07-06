using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService.DeleteFile
{
    public class FileDeleteMessage
    {
        public string FileId { get; init; }

        public FileDeleteMessage()
        {
        }

        public FileDeleteMessage(string fileId)
        {
            FileId = fileId;
        }
    }
}
