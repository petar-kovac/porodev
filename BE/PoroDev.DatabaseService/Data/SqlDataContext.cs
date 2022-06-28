using Microsoft.EntityFrameworkCore;
using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.Database.Data
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions<SqlDataContext> options) : base(options)
        {
        }

        public virtual DbSet<DataUserModel> Users { get; set; } = default!;

        public virtual DbSet<FileData> UserFiles { get; set; } = default!;
    }
}