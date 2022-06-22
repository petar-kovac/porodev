using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Models.RuntimeModels.Data;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<DataUserModel>
    {
        public void Configure(EntityTypeBuilder<DataUserModel> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
