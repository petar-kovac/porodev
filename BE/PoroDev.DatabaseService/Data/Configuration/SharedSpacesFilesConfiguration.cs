using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.SharedSpaces;

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

            builder.HasOne(x => x.File)
                .WithMany(x => x.SharedSpaces)
                .HasForeignKey(x => x.FileId);
        }
    }
}