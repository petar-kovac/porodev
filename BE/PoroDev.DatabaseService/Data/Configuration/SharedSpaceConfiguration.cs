using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.SharedSpaces;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class SharedSpaceConfiguration : IEntityTypeConfiguration<SharedSpace>
    {
        public void Configure(EntityTypeBuilder<SharedSpace> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<DataUserModel>(x => x.DataUserModel)
                .WithMany(x => x.UsersSharedSpaces)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}