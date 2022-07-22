using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.UserModels.Data;
using PoroDev.Common.Models.UserReportsModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class UserReportsConfiguration : IEntityTypeConfiguration<UserReportsData>
    { 
        public void Configure(EntityTypeBuilder<UserReportsData> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<DataUserModel>(x => x.CurrentUser)
                .WithMany(x => x.userReportsDatas)
                .HasForeignKey(x => x.CurrentUserId);
        }
    }
}