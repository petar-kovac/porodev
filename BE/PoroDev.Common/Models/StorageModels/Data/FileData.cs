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

        public byte[] EncryptionKey { get; set; }

        public byte[] Iv { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DataUserModel CurrentUser { get; set; }

        public FileData()
        {
        }

        public FileData(string fileId)
        {
            FileId = fileId;
        }

        public FileData(string fileId, Guid CurrentUserId, bool isDeleted, byte[] encryptionKey, byte[] iv) : this(fileId)
        {
            this.CurrentUserId = CurrentUserId;
            this.IsDeleted = isDeleted;
            EncryptionKey = encryptionKey;
            Iv = iv;
        }
    }
}