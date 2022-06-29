using PoroDev.Common.Models.UserModels.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoroDev.Common.Contracts.StorageService
{
    public class FileData
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(24)]
        public string FileId { get; set; }

        public DataUserModel CurrentUser { get; set; }

        public FileData()
        {
        }

        public FileData(string fileId)
        {
            FileId = fileId;
        }

        public FileData(string fileId, DataUserModel currentUser) : this(fileId)
        {
            CurrentUser = currentUser;
        }
    }
}