using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Contracts.StorageService
{
    public class FileData
    {
        [Key]
        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public DataUserModel CurrentUser { get; set; }

        public FileData()
        {

        }

        public FileData(Guid fileId,  string fileName)
        {
            FileId = fileId;
            FileName = fileName;
        }
    }
}
