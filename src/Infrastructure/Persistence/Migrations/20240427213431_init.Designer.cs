﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(TemplateContext))]
    [Migration("20240427213431_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6023),
                            IsActive = true,
                            Title = "Teknoloji",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6024)
                        },
                        new
                        {
                            Id = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6026),
                            IsActive = true,
                            Title = "Giyim",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6027)
                        });
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

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

                    b.ToTable("products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7470),
                            IsActive = true,
                            Price = 120m,
                            Title = "Klavye",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7470)
                        },
                        new
                        {
                            Id = new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7476),
                            IsActive = false,
                            Price = 250m,
                            Title = "Tyler Durden Poları",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7476)
                        },
                        new
                        {
                            Id = new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                            CategoryId = new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7478),
                            IsActive = true,
                            Price = 300m,
                            Title = "Zen",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7479)
                        },
                        new
                        {
                            Id = new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                            CategoryId = new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7480),
                            IsActive = false,
                            Price = 400m,
                            Title = "Klavye",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7481)
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
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3018),
                            IsActive = true,
                            Name = "admin",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3018)
                        },
                        new
                        {
                            Id = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3024),
                            IsActive = true,
                            Name = "user",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3024)
                        },
                        new
                        {
                            Id = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3026),
                            IsActive = true,
                            Name = "seller",
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3026)
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
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1163),
                            Email = "batuhan@inal.com",
                            FirstName = "Batuhan",
                            IsActive = true,
                            LastName = "Inal",
                            PasswordHash = new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 },
                            PasswordSalt = new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 },
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1165)
                        },
                        new
                        {
                            Id = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1177),
                            Email = "user@user.com",
                            FirstName = "User",
                            IsActive = true,
                            LastName = "User",
                            PasswordHash = new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 },
                            PasswordSalt = new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 },
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1177)
                        },
                        new
                        {
                            Id = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1179),
                            Email = "seller@seller.com",
                            FirstName = "Seller",
                            IsActive = true,
                            LastName = "Seller",
                            PasswordHash = new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 },
                            PasswordSalt = new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 },
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1180)
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
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4275),
                            IsActive = true,
                            RoleId = new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4276),
                            UserId = new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc")
                        },
                        new
                        {
                            Id = new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4282),
                            IsActive = true,
                            RoleId = new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4282),
                            UserId = new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9")
                        },
                        new
                        {
                            Id = new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                            CreatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4285),
                            IsActive = true,
                            RoleId = new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                            UpdatedDate = new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4285),
                            UserId = new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4")
                        });
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Products")
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
                    b.Navigation("Products");
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