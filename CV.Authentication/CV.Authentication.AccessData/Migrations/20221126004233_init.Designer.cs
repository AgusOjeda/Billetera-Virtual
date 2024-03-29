﻿// <auto-generated />
using System;
using CV.Authentication.AccessData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CV.Authentication.AccessData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221126004233_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CV.Authentication.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("PasswordResetToken")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("ResetCode");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime")
                        .HasColumnName("ResetTokenExpires");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasDefaultValue(4)
                        .HasColumnName("State");

                    b.Property<string>("VerificationToken")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasColumnName("VerificationCode");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("VerifiedAt");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("CV.Authentication.Domain.Entities.UserAccountState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Description");

                    b.HasKey("Id");

                    b.ToTable("UserAccountState", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Inactive"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Blocked"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Customer registration in progress"
                        });
                });

            modelBuilder.Entity("CV.Authentication.Domain.Entities.User", b =>
                {
                    b.HasOne("CV.Authentication.Domain.Entities.UserAccountState", "UserAccountState")
                        .WithMany("Users")
                        .HasForeignKey("State")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccountState");
                });

            modelBuilder.Entity("CV.Authentication.Domain.Entities.UserAccountState", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
