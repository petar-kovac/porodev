using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.StorageModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class SharedSpacesFilesConfiguration : IEntityTypeConfiguration<SharedSpacesFiles>
    {
        public void Configure(EntityTypeBuilder<SharedSpacesFiles> builder)
        {
            builder.HasKey(x => new { x.SharedSpaceId, x.FileId });

            builder.HasOne<SharedSpace>(x => x.SharedSpace)
                .WithMany(x => x.SharedSpaceFile)
                .HasForeignKey(x => x.SharedSpaceId);
        }
    }
}
