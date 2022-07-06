﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PoroDev.DatabaseService.Data;

#nullable disable

namespace PoroDev.DatabaseService.Migrations
{
    [DbContext(typeof(SqlDataContext))]
    [Migration("20220706112645_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PoroDev.Common.Models.RuntimeModels.Data.RuntimeData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Arguments")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ExceptionHappened")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ExecutionOutput")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("ExecutionStart")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("ExecutionTime")
                        .HasColumnType("bigint");

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RuntimeMetadata");
                });

            modelBuilder.Entity("PoroDev.Common.Models.StorageModels.Data.FileData", b =>
                {
                    b.Property<string>("FileId")
                        .HasMaxLength(200)
                        .HasColumnType("VARCHAR(200)");

                    b.Property<Guid>("CurrentUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("FileId");

                    b.HasIndex("CurrentUserId");

                    b.ToTable("UserFiles");
                });

            modelBuilder.Entity("PoroDev.Common.Models.UserModels.Data.DataUserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PoroDev.Common.Models.RuntimeModels.Data.RuntimeData", b =>
                {
                    b.HasOne("PoroDev.Common.Models.UserModels.Data.DataUserModel", "User")
                        .WithMany("runtimeDatas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PoroDev.Common.Models.StorageModels.Data.FileData", b =>
                {
                    b.HasOne("PoroDev.Common.Models.UserModels.Data.DataUserModel", "CurrentUser")
                        .WithMany("fileDatas")
                        .HasForeignKey("CurrentUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentUser");
                });

            modelBuilder.Entity("PoroDev.Common.Models.UserModels.Data.DataUserModel", b =>
                {
                    b.Navigation("fileDatas");

                    b.Navigation("runtimeDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
