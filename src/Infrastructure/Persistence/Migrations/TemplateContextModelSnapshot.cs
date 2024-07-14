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

                    b.Property<Guid>("WriterId")
                        .HasColumnType("uuid")
                        .HasColumnName("writer_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("WriterId");

                    b.ToTable("articles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7432),
                            IsActive = true,
                            Title = "Klavye",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7433),
                            WriterId = new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2")
                        },
                        new
                        {
                            Id = new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7439),
                            IsActive = false,
                            Title = "C#",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7439),
                            WriterId = new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2")
                        },
                        new
                        {
                            Id = new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7441),
                            IsActive = true,
                            Title = "C++",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7441),
                            WriterId = new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2")
                        },
                        new
                        {
                            Id = new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            Content = "Content",
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7442),
                            IsActive = false,
                            Title = "Go",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(7443),
                            WriterId = new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2")
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
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(5723),
                            IsActive = true,
                            Title = "Teknoloji",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(5723)
                        },
                        new
                        {
                            Id = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(5725),
                            IsActive = true,
                            Title = "Yazılım",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(5725)
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
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1323),
                            IsActive = true,
                            Name = "admin",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1324)
                        },
                        new
                        {
                            Id = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1328),
                            IsActive = true,
                            Name = "user",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1328)
                        },
                        new
                        {
                            Id = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1332),
                            IsActive = true,
                            Name = "writer",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(1332)
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
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5207),
                            Email = "batuhan@inal.com",
                            FirstName = "Batuhan",
                            IsActive = true,
                            LastName = "Inal",
                            PasswordHash = new byte[] { 37, 203, 14, 109, 227, 73, 210, 71, 112, 237, 183, 238, 213, 28, 199, 154, 59, 165, 157, 62, 54, 254, 209, 243, 139, 209, 140, 171, 40, 38, 188, 242 },
                            PasswordSalt = new byte[] { 108, 140, 207, 120, 169, 236, 65, 72, 5, 234, 255, 80, 110, 231, 87, 237, 6, 188, 242, 128, 78, 65, 214, 214, 234, 112, 44, 160, 124, 173, 151, 73, 54, 202, 51, 48, 215, 35, 141, 173, 185, 37, 77, 156, 146, 117, 103, 181, 15, 231, 94, 246, 225, 200, 221, 87, 43, 56, 133, 108, 125, 93, 184, 183 },
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5209)
                        },
                        new
                        {
                            Id = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5215),
                            Email = "user@user.com",
                            FirstName = "User",
                            IsActive = true,
                            LastName = "User",
                            PasswordHash = new byte[] { 37, 203, 14, 109, 227, 73, 210, 71, 112, 237, 183, 238, 213, 28, 199, 154, 59, 165, 157, 62, 54, 254, 209, 243, 139, 209, 140, 171, 40, 38, 188, 242 },
                            PasswordSalt = new byte[] { 108, 140, 207, 120, 169, 236, 65, 72, 5, 234, 255, 80, 110, 231, 87, 237, 6, 188, 242, 128, 78, 65, 214, 214, 234, 112, 44, 160, 124, 173, 151, 73, 54, 202, 51, 48, 215, 35, 141, 173, 185, 37, 77, 156, 146, 117, 103, 181, 15, 231, 94, 246, 225, 200, 221, 87, 43, 56, 133, 108, 125, 93, 184, 183 },
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5215)
                        },
                        new
                        {
                            Id = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5217),
                            Email = "writer@writer.com",
                            FirstName = "Writer",
                            IsActive = true,
                            LastName = "Writer",
                            PasswordHash = new byte[] { 37, 203, 14, 109, 227, 73, 210, 71, 112, 237, 183, 238, 213, 28, 199, 154, 59, 165, 157, 62, 54, 254, 209, 243, 139, 209, 140, 171, 40, 38, 188, 242 },
                            PasswordSalt = new byte[] { 108, 140, 207, 120, 169, 236, 65, 72, 5, 234, 255, 80, 110, 231, 87, 237, 6, 188, 242, 128, 78, 65, 214, 214, 234, 112, 44, 160, 124, 173, 151, 73, 54, 202, 51, 48, 215, 35, 141, 173, 185, 37, 77, 156, 146, 117, 103, 181, 15, 231, 94, 246, 225, 200, 221, 87, 43, 56, 133, 108, 125, 93, 184, 183 },
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(5218)
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
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3473),
                            IsActive = true,
                            RoleId = new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3474),
                            UserId = new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc")
                        },
                        new
                        {
                            Id = new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3479),
                            IsActive = true,
                            RoleId = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3479),
                            UserId = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9")
                        },
                        new
                        {
                            Id = new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3481),
                            IsActive = true,
                            RoleId = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 147, DateTimeKind.Utc).AddTicks(3481),
                            UserId = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Writer", b =>
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

                    b.Property<byte>("Level")
                        .HasColumnType("smallint")
                        .HasColumnName("level");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nick");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("writers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                            CreatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(8223),
                            IsActive = true,
                            Level = (byte)4,
                            Nick = "default-user",
                            UpdatedDate = new DateTime(2024, 7, 14, 12, 56, 59, 146, DateTimeKind.Utc).AddTicks(8224),
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

                    b.HasOne("Domain.Entities.Writer", "Writer")
                        .WithMany("Articles")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Writer");
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

            modelBuilder.Entity("Domain.Entities.Writer", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("Domain.Entities.Writer", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
