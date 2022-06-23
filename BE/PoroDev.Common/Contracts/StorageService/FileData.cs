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
        public Guid FileId { get; set; }

        public Guid UserId { get; set; }

        // We just need UserID from this model
        public DataUserModel User { get; set; }

        public FileData(Guid fileId, Guid userId)
        {
            FileId = fileId;
            UserId = userId;

        }
    }
}
