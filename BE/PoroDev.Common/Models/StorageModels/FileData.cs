using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.StorageModels
{
    public class FileData
    {
        public ObjectId FileId { get; set; }

        public Guid UserId { get; set; }

        // We just need UserID from this model
        public DataUserModel User { get; set; }

        public FileData(ObjectId fileId, Guid userId)
        {
            FileId = fileId;
            UserId = userId;

        }
    }
}
