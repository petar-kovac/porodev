using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;

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
        public virtual DbSet<SharedSpace> SharedSpace { get; set; } = default!;
        public virtual DbSet<SharedSpacesUsers> SharedSpacesUsers { get; set; } = default!;
        public virtual DbSet<SharedSpacesFiles> SharedSpacesFiles { get; set; } = default!;
        public virtual DbSet<NotificationDataModel> NotificationData { get; set; } = default!;
        public virtual DbSet<UserReportsData> UserReportsData { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlDataContext).Assembly);
        }
    }
}