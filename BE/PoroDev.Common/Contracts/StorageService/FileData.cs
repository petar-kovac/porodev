using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService
{
    public class FileData
    {
        public Guid UserId { get; set; }

        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public DataUserModel CurrentUser { get; set; }

        public FileData()
        {

        }

        public FileData(Guid userId, Guid fileId,  string fileName)
        {
            UserId = userId;
            FileId = fileId;
            FileName = fileName;
        }
    }
}
