﻿using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PoroDev.Common.Models.StorageModels.Data
{
    public class FileData
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string FileId { get; set; }

        public Guid CurrentUserId { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel CurrentUser { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<SharedSpacesFiles> SharedSpaces { get; set; }

        public bool IsExecutable { get; set; }

        public FileData()
        {
            
        }

        public FileData(string fileId)
        {
            FileId = fileId;
        }

        public FileData(string fileId, Guid CurrentUserId, bool isDeleted, bool isExecutable = false) : this(fileId)
        {
            this.CurrentUserId = CurrentUserId;
            this.IsDeleted = isDeleted;
            IsExecutable = isExecutable;
        }
    }
}