using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class user_writer_favorite_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_writer_favorites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    writer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_writer_favorites", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_writer_favorites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_writer_favorites_writers_writer_id",
                        column: x => x.writer_id,
                        principalTable: "writers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2164), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2165) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2176), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2176) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2172), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2172) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2174), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(2175) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(967), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(967) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(962), new DateTime(2024, 10, 20, 12, 20, 34, 705, DateTimeKind.Utc).AddTicks(964) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8158), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8158) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8161), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8162) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8154), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8154) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8810), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8811) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8818), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8819) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8816), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(8816) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5159), new byte[] { 117, 232, 2, 173, 67, 197, 141, 17, 18, 35, 253, 71, 19, 93, 224, 88, 245, 189, 135, 250, 17, 188, 59, 182, 123, 179, 82, 84, 126, 105, 246, 4 }, new byte[] { 92, 251, 153, 135, 248, 21, 156, 102, 198, 27, 71, 164, 241, 25, 124, 28, 25, 37, 48, 209, 250, 123, 163, 112, 140, 7, 77, 242, 9, 50, 224, 31, 46, 122, 117, 36, 189, 245, 168, 248, 104, 230, 187, 153, 186, 45, 61, 151, 207, 148, 189, 236, 49, 217, 157, 234, 245, 250, 122, 193, 115, 68, 118, 227 }, new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5159) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5146), new byte[] { 117, 232, 2, 173, 67, 197, 141, 17, 18, 35, 253, 71, 19, 93, 224, 88, 245, 189, 135, 250, 17, 188, 59, 182, 123, 179, 82, 84, 126, 105, 246, 4 }, new byte[] { 92, 251, 153, 135, 248, 21, 156, 102, 198, 27, 71, 164, 241, 25, 124, 28, 25, 37, 48, 209, 250, 123, 163, 112, 140, 7, 77, 242, 9, 50, 224, 31, 46, 122, 117, 36, 189, 245, 168, 248, 104, 230, 187, 153, 186, 45, 61, 151, 207, 148, 189, 236, 49, 217, 157, 234, 245, 250, 122, 193, 115, 68, 118, 227 }, new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5148) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5164), new byte[] { 117, 232, 2, 173, 67, 197, 141, 17, 18, 35, 253, 71, 19, 93, 224, 88, 245, 189, 135, 250, 17, 188, 59, 182, 123, 179, 82, 84, 126, 105, 246, 4 }, new byte[] { 92, 251, 153, 135, 248, 21, 156, 102, 198, 27, 71, 164, 241, 25, 124, 28, 25, 37, 48, 209, 250, 123, 163, 112, 140, 7, 77, 242, 9, 50, 224, 31, 46, 122, 117, 36, 189, 245, 168, 248, 104, 230, 187, 153, 186, 45, 61, 151, 207, 148, 189, 236, 49, 217, 157, 234, 245, 250, 122, 193, 115, 68, 118, 227 }, new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(5164) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(7196), new DateTime(2024, 10, 20, 12, 20, 34, 704, DateTimeKind.Utc).AddTicks(7197) });

            migrationBuilder.CreateIndex(
                name: "IX_user_writer_favorites_user_id",
                table: "user_writer_favorites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_writer_favorites_writer_id",
                table: "user_writer_favorites",
                column: "writer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_writer_favorites");

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
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4565), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4566) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4561), new DateTime(2024, 10, 5, 12, 38, 5, 180, DateTimeKind.Utc).AddTicks(4562) });

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
        }
    }
}
