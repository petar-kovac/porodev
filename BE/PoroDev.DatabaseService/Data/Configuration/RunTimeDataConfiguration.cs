using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class RunTimeDataConfiguration : IEntityTypeConfiguration<RuntimeData>
    {
        public void Configure(EntityTypeBuilder<RuntimeData> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<DataUserModel>(x => x.User)
                .WithMany(x => x.runtimeDatas)
                .HasForeignKey(x => x.UserId);
        }
    }
}