using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class article_countinfo_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fav_count",
                table: "articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "like_count",
                table: "articles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8512), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8512) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8520), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8520) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8516), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8516) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8518), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(8519) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(7574), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(7574) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(7572), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(7573) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6209), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6211), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6212) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6205), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6205) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6853), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6854) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6860), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6860) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6858), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(6858) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4091), new byte[] { 41, 241, 147, 45, 194, 145, 240, 56, 175, 8, 218, 17, 128, 224, 183, 136, 174, 144, 9, 140, 234, 19, 205, 86, 11, 194, 15, 101, 113, 235, 57, 4 }, new byte[] { 53, 249, 245, 84, 229, 117, 198, 106, 183, 146, 148, 164, 202, 255, 90, 194, 63, 90, 249, 27, 221, 212, 245, 154, 68, 43, 180, 23, 134, 227, 247, 215, 70, 130, 119, 206, 125, 32, 100, 228, 146, 133, 124, 191, 195, 231, 210, 42, 200, 102, 221, 225, 235, 29, 50, 188, 189, 208, 73, 18, 179, 78, 205, 193 }, new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4083), new byte[] { 41, 241, 147, 45, 194, 145, 240, 56, 175, 8, 218, 17, 128, 224, 183, 136, 174, 144, 9, 140, 234, 19, 205, 86, 11, 194, 15, 101, 113, 235, 57, 4 }, new byte[] { 53, 249, 245, 84, 229, 117, 198, 106, 183, 146, 148, 164, 202, 255, 90, 194, 63, 90, 249, 27, 221, 212, 245, 154, 68, 43, 180, 23, 134, 227, 247, 215, 70, 130, 119, 206, 125, 32, 100, 228, 146, 133, 124, 191, 195, 231, 210, 42, 200, 102, 221, 225, 235, 29, 50, 188, 189, 208, 73, 18, 179, 78, 205, 193 }, new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4085) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4093), new byte[] { 41, 241, 147, 45, 194, 145, 240, 56, 175, 8, 218, 17, 128, 224, 183, 136, 174, 144, 9, 140, 234, 19, 205, 86, 11, 194, 15, 101, 113, 235, 57, 4 }, new byte[] { 53, 249, 245, 84, 229, 117, 198, 106, 183, 146, 148, 164, 202, 255, 90, 194, 63, 90, 249, 27, 221, 212, 245, 154, 68, 43, 180, 23, 134, 227, 247, 215, 70, 130, 119, 206, 125, 32, 100, 228, 146, 133, 124, 191, 195, 231, 210, 42, 200, 102, 221, 225, 235, 29, 50, 188, 189, 208, 73, 18, 179, 78, 205, 193 }, new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(4093) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(5348), new DateTime(2024, 7, 31, 20, 37, 36, 847, DateTimeKind.Utc).AddTicks(5348) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fav_count",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "like_count",
                table: "articles");

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8660), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8661) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8670), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8666), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8666) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8668), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7920), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7918), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7918) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6603), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6603) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6604), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6605) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6598), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6599) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7206), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7206) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7211), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7212) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7209), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7210) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4550), new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4540), new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4542) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4552), new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4552) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(5735), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(5736) });
        }
    }
}
