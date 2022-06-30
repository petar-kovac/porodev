using PoroDev.Common.Models.UserModels.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoroDev.Common.Models.StorageModels.Data
{
    public class FileData
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string FileId { get; set; }

        public Guid CurrentUserId { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel CurrentUser { get; set; }

        public FileData()
        {
        }

        public FileData(string fileId)
        {
            FileId = fileId;
        }

        public FileData(string fileId, Guid CurrentUserId) : this(fileId)
        {
            this.CurrentUserId = CurrentUserId;
        }
    }
}
