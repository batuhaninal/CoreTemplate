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
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_date", "is_active", "title", "updated_date" },
                values: new object[,]
                {
                    { new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6026), true, "Giyim", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6027) },
                    { new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6023), true, "Teknoloji", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(6024) }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_date", "is_active", "name", "updated_date" },
                values: new object[,]
                {
                    { new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3024), true, "user", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3024) },
                    { new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3026), true, "seller", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3026) },
                    { new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3018), true, "admin", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(3018) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_date", "email", "first_name", "is_active", "last_name", "password_hash", "password_salt", "updated_date" },
                values: new object[,]
                {
                    { new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1177), "user@user.com", "User", true, "User", new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 }, new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 }, new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1177) },
                    { new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1163), "batuhan@inal.com", "Batuhan", true, "Inal", new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 }, new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 }, new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1165) },
                    { new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1179), "seller@seller.com", "Seller", true, "Seller", new byte[] { 25, 109, 130, 62, 198, 37, 178, 81, 57, 207, 121, 118, 24, 41, 92, 78, 109, 115, 193, 144, 186, 37, 139, 220, 149, 219, 38, 208, 197, 42, 189, 48 }, new byte[] { 43, 73, 152, 7, 96, 233, 22, 14, 173, 178, 65, 33, 205, 55, 200, 120, 4, 71, 102, 119, 235, 69, 155, 151, 253, 125, 240, 126, 87, 234, 52, 117, 95, 159, 194, 231, 39, 106, 74, 79, 172, 35, 65, 87, 8, 254, 198, 59, 201, 245, 192, 160, 85, 179, 59, 148, 252, 90, 83, 46, 48, 225, 88, 60 }, new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(1180) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "category_id", "created_date", "is_active", "price", "title", "updated_date" },
                values: new object[,]
                {
                    { new Guid("29e2d55d-fbd2-4f0c-b71e-357d2b7ffe88"), new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7470), true, 120m, "Klavye", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7470) },
                    { new Guid("5836c09f-c947-4222-9cfb-5f665b83f755"), new Guid("24fe2676-c6b0-4f15-b045-edd9a84a7ca7"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7480), false, 400m, "Klavye", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7481) },
                    { new Guid("5d11e4f2-1db7-4667-9a90-87918dd73569"), new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7476), false, 250m, "Tyler Durden Poları", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7476) },
                    { new Guid("f4edf481-d457-4e3e-a670-0b52635744df"), new Guid("1fe6dbd9-048f-45cf-b1ea-d46210a87d96"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7478), true, 300m, "Zen", new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(7479) }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_date", "is_active", "role_id", "updated_date", "user_id" },
                values: new object[,]
                {
                    { new Guid("0e398d5d-c49e-4b68-8da1-9616a0145a6d"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4275), true, new Guid("cf2a3f8d-88bc-4c0c-a5e7-b5f9dd20658b"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4276), new Guid("f219d021-5d29-4e63-8250-4aa1e514d8dc") },
                    { new Guid("1fca6ef1-27de-4fe4-9b8b-faebc2150d43"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4285), true, new Guid("83e5c9f0-7e6d-4a08-a515-2e8889f3b140"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4285), new Guid("f3c72d95-d69b-478b-a186-7934a9bf87a4") },
                    { new Guid("8c056215-aa82-4ed8-bf86-b150a3e0fcf7"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4282), true, new Guid("1e9d831e-fb57-4c7a-b8d5-8a4a0fb1f7b2"), new DateTime(2024, 4, 27, 21, 34, 31, 327, DateTimeKind.Utc).AddTicks(4282), new Guid("ca9a97c7-6149-4e89-a5c3-61928510c2b9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_user_id",
                table: "user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
