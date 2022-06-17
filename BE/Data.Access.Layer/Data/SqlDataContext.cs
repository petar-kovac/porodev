using Data.Access.Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Access.Layer.Data
{
    public class SqlDataContext : DbContext
    {
        public SqlDataContext(DbContextOptions<SqlDataContext> options) : base(options)
        {
        }

        public virtual DbSet<DataUserModel> Users { get; set; } = default!;
    }
}