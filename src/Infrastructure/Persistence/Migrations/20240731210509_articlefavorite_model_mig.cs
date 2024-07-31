using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class articlefavorite_model_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article_favorites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_favorites", x => x.id);
                    table.ForeignKey(
                        name: "FK_article_favorites_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_article_favorites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7883), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7883) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7894), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7894) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7889), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7889) });

            migrationBuilder.UpdateData(
                table: "articles",
                keyColumn: "id",
                keyValue: new Guid("f4edf481-d457-4e3e-a670-0b52635744df"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7892), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(7892) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6758), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6758) });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6756), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6756) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5317), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5318) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5320), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5313), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5314) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5996), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(5996) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6005), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6005) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6001), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(6001) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3186), new byte[] { 51, 160, 51, 101, 92, 3, 60, 129, 92, 48, 33, 223, 46, 209, 44, 250, 225, 15, 92, 121, 211, 162, 125, 181, 158, 135, 244, 216, 149, 130, 163, 233 }, new byte[] { 188, 87, 215, 186, 193, 112, 33, 123, 32, 167, 117, 116, 126, 83, 137, 186, 19, 251, 95, 242, 141, 201, 131, 255, 10, 224, 17, 220, 8, 47, 33, 217, 120, 97, 36, 120, 198, 134, 79, 251, 197, 154, 209, 142, 228, 146, 168, 22, 122, 28, 210, 102, 104, 124, 255, 43, 6, 176, 232, 224, 213, 197, 252, 97 }, new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3187) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3177), new byte[] { 51, 160, 51, 101, 92, 3, 60, 129, 92, 48, 33, 223, 46, 209, 44, 250, 225, 15, 92, 121, 211, 162, 125, 181, 158, 135, 244, 216, 149, 130, 163, 233 }, new byte[] { 188, 87, 215, 186, 193, 112, 33, 123, 32, 167, 117, 116, 126, 83, 137, 186, 19, 251, 95, 242, 141, 201, 131, 255, 10, 224, 17, 220, 8, 47, 33, 217, 120, 97, 36, 120, 198, 134, 79, 251, 197, 154, 209, 142, 228, 146, 168, 22, 122, 28, 210, 102, 104, 124, 255, 43, 6, 176, 232, 224, 213, 197, 252, 97 }, new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"),
                columns: new[] { "created_date", "password_hash", "password_salt", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3189), new byte[] { 51, 160, 51, 101, 92, 3, 60, 129, 92, 48, 33, 223, 46, 209, 44, 250, 225, 15, 92, 121, 211, 162, 125, 181, 158, 135, 244, 216, 149, 130, 163, 233 }, new byte[] { 188, 87, 215, 186, 193, 112, 33, 123, 32, 167, 117, 116, 126, 83, 137, 186, 19, 251, 95, 242, 141, 201, 131, 255, 10, 224, 17, 220, 8, 47, 33, 217, 120, 97, 36, 120, 198, 134, 79, 251, 197, 154, 209, 142, 228, 146, 168, 22, 122, 28, 210, 102, 104, 124, 255, 43, 6, 176, 232, 224, 213, 197, 252, 97 }, new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(3189) });

            migrationBuilder.UpdateData(
                table: "writers",
                keyColumn: "id",
                keyValue: new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"),
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(4405), new DateTime(2024, 7, 31, 21, 5, 9, 290, DateTimeKind.Utc).AddTicks(4406) });

            migrationBuilder.CreateIndex(
                name: "IX_article_favorites_article_id",
                table: "article_favorites",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_favorites_user_id",
                table: "article_favorites",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_favorites");

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
