using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class category_children_relations_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5748), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5748) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5759), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5754), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5754) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5757), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(5757) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "parent_id", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4565), new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4566) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "parent_id", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4561), null, new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4562) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(133), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(133) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(135), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(135) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(127), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(128) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2811), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2811) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2820), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2820) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2818), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(2818) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4167), new byte[] { 121, 145, 91, 26, 57, 222, 246, 90, 206, 76, 13, 117, 57, 47, 208, 4, 34, 228, 253, 212, 163, 189, 134, 149, 11, 7, 147, 93, 220, 60, 166, 154 }, new byte[] { 75, 226, 228, 215, 205, 183, 226, 50, 255, 50, 58, 91, 157, 137, 127, 19, 223, 104, 32, 20, 8, 8, 167, 244, 23, 80, 88, 149, 158, 143, 137, 249, 33, 217, 194, 65, 186, 65, 203, 45, 125, 178, 63, 122, 128, 226, 19, 81, 116, 146, 112, 21, 37, 21, 86, 121, 208, 118, 221, 66, 65, 161, 0, 7 }, new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4167) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4157), new byte[] { 121, 145, 91, 26, 57, 222, 246, 90, 206, 76, 13, 117, 57, 47, 208, 4, 34, 228, 253, 212, 163, 189, 134, 149, 11, 7, 147, 93, 220, 60, 166, 154 }, new byte[] { 75, 226, 228, 215, 205, 183, 226, 50, 255, 50, 58, 91, 157, 137, 127, 19, 223, 104, 32, 20, 8, 8, 167, 244, 23, 80, 88, 149, 158, 143, 137, 249, 33, 217, 194, 65, 186, 65, 203, 45, 125, 178, 63, 122, 128, 226, 19, 81, 116, 146, 112, 21, 37, 21, 86, 121, 208, 118, 221, 66, 65, 161, 0, 7 }, new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4159) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4169), new byte[] { 121, 145, 91, 26, 57, 222, 246, 90, 206, 76, 13, 117, 57, 47, 208, 4, 34, 228, 253, 212, 163, 189, 134, 149, 11, 7, 147, 93, 220, 60, 166, 154 }, new byte[] { 75, 226, 228, 215, 205, 183, 226, 50, 255, 50, 58, 91, 157, 137, 127, 19, 223, 104, 32, 20, 8, 8, 167, 244, 23, 80, 88, 149, 158, 143, 137, 249, 33, 217, 194, 65, 186, 65, 203, 45, 125, 178, 63, 122, 128, 226, 19, 81, 116, 146, 112, 21, 37, 21, 86, 121, 208, 118, 221, 66, 65, 161, 0, 7 }, new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(4169) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(7825), new DateTime(2024, 10, 5, 12, 38, 5, 179, DateTimeKind.Utc).AddTicks(7826) });

            migrationBuilder.CreateIndex(
                name: "IX_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_parent_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8362), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8362) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8371), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8367), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8367) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8369), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(7131), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(7131) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(7129), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5467), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5468) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5469), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5463), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(5464) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6211), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6211) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6218), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6219) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6216), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3098), new byte[] { 205, 190, 53, 155, 213, 60, 78, 30, 58, 156, 203, 227, 227, 182, 78, 182, 238, 69, 97, 254, 146, 156, 234, 51, 255, 46, 81, 213, 213, 93, 5, 95 }, new byte[] { 96, 58, 30, 77, 249, 113, 229, 81, 89, 236, 234, 183, 20, 80, 211, 149, 100, 105, 9, 44, 125, 240, 50, 157, 52, 252, 34, 193, 99, 99, 196, 177, 2, 64, 216, 172, 191, 10, 58, 158, 42, 167, 7, 200, 230, 3, 19, 88, 10, 35, 229, 210, 72, 92, 45, 113, 218, 143, 205, 209, 102, 103, 149, 175 }, new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3099) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3089), new byte[] { 205, 190, 53, 155, 213, 60, 78, 30, 58, 156, 203, 227, 227, 182, 78, 182, 238, 69, 97, 254, 146, 156, 234, 51, 255, 46, 81, 213, 213, 93, 5, 95 }, new byte[] { 96, 58, 30, 77, 249, 113, 229, 81, 89, 236, 234, 183, 20, 80, 211, 149, 100, 105, 9, 44, 125, 240, 50, 157, 52, 252, 34, 193, 99, 99, 196, 177, 2, 64, 216, 172, 191, 10, 58, 158, 42, 167, 7, 200, 230, 3, 19, 88, 10, 35, 229, 210, 72, 92, 45, 113, 218, 143, 205, 209, 102, 103, 149, 175 }, new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3101), new byte[] { 205, 190, 53, 155, 213, 60, 78, 30, 58, 156, 203, 227, 227, 182, 78, 182, 238, 69, 97, 254, 146, 156, 234, 51, 255, 46, 81, 213, 213, 93, 5, 95 }, new byte[] { 96, 58, 30, 77, 249, 113, 229, 81, 89, 236, 234, 183, 20, 80, 211, 149, 100, 105, 9, 44, 125, 240, 50, 157, 52, 252, 34, 193, 99, 99, 196, 177, 2, 64, 216, 172, 191, 10, 58, 158, 42, 167, 7, 200, 230, 3, 19, 88, 10, 35, 229, 210, 72, 92, 45, 113, 218, 143, 205, 209, 102, 103, 149, 175 }, new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(3101) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(4466), new DateTime(2024, 9, 8, 15, 42, 45, 229, DateTimeKind.Utc).AddTicks(4467) });
        }
    }
}
