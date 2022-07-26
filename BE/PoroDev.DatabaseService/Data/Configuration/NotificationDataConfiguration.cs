using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.NotificationServiceModels;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class NotificationDataConfiguration : IEntityTypeConfiguration<NotificationDataModel>
    {
        public void Configure(EntityTypeBuilder<NotificationDataModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<DataUserModel>(notificationTime => notificationTime.User)
                .WithOne(user => user.NotificationTime)
                .HasForeignKey<NotificationDataModel>(x => x.UserId);
        }
    }
}
