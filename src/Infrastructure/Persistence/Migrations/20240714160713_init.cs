using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    first_name = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    last_name = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "writers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nick = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    level = table.Column<byte>(type: "smallint", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_writers", x => x.id);
                    table.ForeignKey(
                        name: "FK_writers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    writer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                    table.ForeignKey(
                        name: "FK_articles_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articles_writers_writer_id",
                        column: x => x.writer_id,
                        principalTable: "writers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_date", "is_active", "title", "updated_date" },
                values: new object[,]
                {
                    { new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7920), true, "Yazılım", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7920) },
                    { new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7918), true, "Teknoloji", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7918) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_date", "is_active", "name", "updated_date" },
                values: new object[,]
                {
                    { new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6603), true, "user", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6603) },
                    { new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6604), true, "writer", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6605) },
                    { new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6598), true, "admin", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(6599) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_date", "email", "first_name", "is_active", "last_name", "password_hash", "password_salt", "updated_date" },
                values: new object[,]
                {
                    { new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4550), "user@user.com", "User", true, "User", new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4550) },
                    { new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4540), "batuhan@inal.com", "Batuhan", true, "Inal", new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4542) },
                    { new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4552), "writer@writer.com", "Writer", true, "Writer", new byte[] { 148, 171, 13, 115, 109, 177, 129, 178, 152, 75, 238, 217, 38, 102, 124, 68, 72, 144, 63, 99, 201, 86, 240, 192, 61, 253, 187, 231, 254, 196, 171, 194 }, new byte[] { 67, 194, 236, 79, 221, 34, 171, 209, 100, 216, 100, 166, 5, 71, 235, 235, 205, 44, 104, 83, 115, 160, 51, 210, 43, 183, 226, 170, 164, 175, 219, 40, 149, 242, 13, 168, 255, 53, 195, 218, 251, 43, 198, 253, 226, 155, 136, 129, 174, 77, 41, 1, 88, 119, 44, 190, 186, 89, 50, 42, 172, 207, 228, 180 }, new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(4552) }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_date", "is_active", "role_id", "updated_date", "user_id" },
                values: new object[,]
                {
                    { new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7206), true, new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7206), new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc") },
                    { new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7211), true, new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7212), new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4") },
                    { new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7209), true, new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(7210), new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9") }
                });

            migrationBuilder.InsertData(
                table: "writers",
                columns: new[] { "id", "created_date", "is_active", "level", "nick", "updated_date", "user_id" },
                values: new object[] { new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2"), new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(5735), true, (byte)4, "default-user", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(5736), new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4") });

            migrationBuilder.InsertData(
                table: "articles",
                columns: new[] { "id", "category_id", "content", "created_date", "is_active", "title", "updated_date", "writer_id" },
                values: new object[,]
                {
                    { new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"), new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), "Content", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8660), true, "Klavye", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8661), new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2") },
                    { new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"), new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), "Content", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8670), false, "Go", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8670), new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2") },
                    { new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"), new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), "Content", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8666), false, "C#", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8666), new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2") },
                    { new Guid("f4edf481-d457-4e3e-a670-0b52635744df"), new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), "Content", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8668), true, "C++", new DateTime(2024, 7, 14, 16, 7, 13, 124, DateTimeKind.Utc).AddTicks(8668), new Guid("7e137c28-9868-4e00-b2bd-73ab46e43bc2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_category_id",
                table: "articles",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_articles_writer_id",
                table: "articles",
                column: "writer_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_writers_user_id",
                table: "writers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "writers");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
