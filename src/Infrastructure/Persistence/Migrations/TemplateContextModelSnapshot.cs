﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(TemplateContext))]
    partial class TemplateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("articles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7477),
                            IsActive = true,
                            Title = "Klavye",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7478)
                        },
                        new
                        {
                            Id = new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7481),
                            IsActive = false,
                            Title = "C#",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7481)
                        },
                        new
                        {
                            Id = new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7484),
                            IsActive = true,
                            Title = "C++",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7484)
                        },
                        new
                        {
                            Id = new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7486),
                            IsActive = false,
                            Title = "Go",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(7486)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(6044),
                            IsActive = true,
                            Title = "Teknoloji",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(6044)
                        },
                        new
                        {
                            Id = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(6046),
                            IsActive = true,
                            Title = "Yazılım",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(6046)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2474),
                            IsActive = true,
                            Name = "admin",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2475)
                        },
                        new
                        {
                            Id = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2480),
                            IsActive = true,
                            Name = "user",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2480)
                        },
                        new
                        {
                            Id = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2482),
                            IsActive = true,
                            Name = "seller",
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(2482)
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)")
                        .HasColumnName("last_name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_salt");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(90),
                            Email = "batuhan@inal.com",
                            FirstName = "Batuhan",
                            IsActive = true,
                            LastName = "Inal",
                            PasswordHash = new byte[] { 95, 63, 23, 245, 167, 127, 46, 148, 210, 53, 212, 45, 159, 128, 54, 164, 171, 160, 159, 27, 226, 234, 21, 224, 66, 12, 193, 49, 238, 82, 51, 33 },
                            PasswordSalt = new byte[] { 91, 228, 135, 63, 153, 60, 255, 84, 74, 35, 230, 186, 7, 50, 243, 107, 228, 113, 241, 106, 165, 31, 53, 86, 42, 81, 41, 7, 38, 153, 42, 254, 170, 42, 88, 73, 170, 130, 124, 18, 59, 30, 55, 188, 13, 29, 42, 94, 151, 147, 181, 165, 49, 254, 201, 216, 44, 87, 231, 169, 7, 44, 176, 173 },
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(98)
                        },
                        new
                        {
                            Id = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(106),
                            Email = "user@user.com",
                            FirstName = "User",
                            IsActive = true,
                            LastName = "User",
                            PasswordHash = new byte[] { 95, 63, 23, 245, 167, 127, 46, 148, 210, 53, 212, 45, 159, 128, 54, 164, 171, 160, 159, 27, 226, 234, 21, 224, 66, 12, 193, 49, 238, 82, 51, 33 },
                            PasswordSalt = new byte[] { 91, 228, 135, 63, 153, 60, 255, 84, 74, 35, 230, 186, 7, 50, 243, 107, 228, 113, 241, 106, 165, 31, 53, 86, 42, 81, 41, 7, 38, 153, 42, 254, 170, 42, 88, 73, 170, 130, 124, 18, 59, 30, 55, 188, 13, 29, 42, 94, 151, 147, 181, 165, 49, 254, 201, 216, 44, 87, 231, 169, 7, 44, 176, 173 },
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(107)
                        },
                        new
                        {
                            Id = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(109),
                            Email = "seller@seller.com",
                            FirstName = "Seller",
                            IsActive = true,
                            LastName = "Seller",
                            PasswordHash = new byte[] { 95, 63, 23, 245, 167, 127, 46, 148, 210, 53, 212, 45, 159, 128, 54, 164, 171, 160, 159, 27, 226, 234, 21, 224, 66, 12, 193, 49, 238, 82, 51, 33 },
                            PasswordSalt = new byte[] { 91, 228, 135, 63, 153, 60, 255, 84, 74, 35, 230, 186, 7, 50, 243, 107, 228, 113, 241, 106, 165, 31, 53, 86, 42, 81, 41, 7, 38, 153, 42, 254, 170, 42, 88, 73, 170, 130, 124, 18, 59, 30, 55, 188, 13, 29, 42, 94, 151, 147, 181, 165, 49, 254, 201, 216, 44, 87, 231, 169, 7, 44, 176, 173 },
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(109)
                        });
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4506),
                            IsActive = true,
                            RoleId = new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4507),
                            UserId = new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc")
                        },
                        new
                        {
                            Id = new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4513),
                            IsActive = true,
                            RoleId = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4513),
                            UserId = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9")
                        },
                        new
                        {
                            Id = new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                            CreatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4516),
                            IsActive = true,
                            RoleId = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            UpdatedDate = new DateTime(2024, 5, 9, 20, 30, 25, 816, DateTimeKind.Utc).AddTicks(4516),
                            UserId = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Article", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
