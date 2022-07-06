using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions<SqlDataContext> options) : base(options)
        {
        }

        public virtual DbSet<DataUserModel> Users { get; set; } = default!;
        public virtual DbSet<RuntimeData> RuntimeMetadata { get; set; } = default!;
        public virtual DbSet<FileData> UserFiles { get; set; } = default!;
    }
}