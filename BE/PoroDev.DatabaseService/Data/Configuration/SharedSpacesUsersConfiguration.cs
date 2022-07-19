using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.StorageModels.Data;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class SharedSpacesUsersConfiguration : IEntityTypeConfiguration<SharedSpacesUsers>
    {
        public void Configure(EntityTypeBuilder<SharedSpacesUsers> builder)
        {
            builder.HasKey(x => new { x.SharedSpaceId, x.UserId });

            builder.HasOne<SharedSpace>(x => x.SharedSpace)
                .WithMany(x => x.SharedSpaceUser)
                .HasForeignKey(x => x.SharedSpaceId);

            builder.HasOne<DataUserModel>(x => x.User)
                .WithMany(x => x.SharedSpaceUser)
                .HasForeignKey(x => x.UserId);
        }
    }
}
