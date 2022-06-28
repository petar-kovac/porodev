﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoroDev.Common.Contracts.StorageService;
using PoroDev.Common.Models.UserModels.Data;

namespace PoroDev.DatabaseService.Data.Configuration
{
    public class StorageDataConfiguration : IEntityTypeConfiguration<FileData>
    {
        public void Configure(EntityTypeBuilder<FileData> builder)
        {
            builder.HasKey(x => x.FileDataId);

            builder.HasOne<DataUserModel>(x => x.User)
                .WithMany(x => x.fileDatas)
                .HasForeignKey(x => x.UserId);
        }
    }
}
