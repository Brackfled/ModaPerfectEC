using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("aa6bab7e-a949-4756-b869-4d9fb6e4598d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("107668af-ab09-4e5d-ba68-f7341dc7a372"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MPFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MPFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MPFile_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockAmount = table.Column<int>(type: "int", nullable: false),
                    Sizes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 479, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Auth.UpdateUserState", null },
                    { 480, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Admin", null },
                    { 481, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Read", null },
                    { 482, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Write", null },
                    { 483, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Create", null },
                    { 484, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Update", null },
                    { 485, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Categories.Delete", null },
                    { 486, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Admin", null },
                    { 487, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Read", null },
                    { 488, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Write", null },
                    { 489, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Create", null },
                    { 490, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Update", null },
                    { 491, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SubCategories.Delete", null },
                    { 492, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Admin", null },
                    { 493, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Read", null },
                    { 494, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Write", null },
                    { 495, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Create", null },
                    { 496, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Update", null },
                    { 497, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Products.Delete", null },
                    { 498, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Admin", null },
                    { 499, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Read", null },
                    { 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Write", null },
                    { 501, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Create", null },
                    { 502, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Update", null },
                    { 503, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductVariants.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 223, 104, 176, 222, 234, 236, 58, 71, 145, 50, 244, 136, 88, 103, 83, 169, 79, 132, 179, 151, 21, 35, 5, 19, 195, 152, 136, 100, 171, 63, 10, 12, 196, 2, 198, 55, 59, 175, 205, 7, 167, 193, 127, 139, 107, 178, 37, 152, 75, 233, 167, 155, 234, 69, 216, 195, 74, 16, 121, 2, 187, 171, 132, 167 }, new byte[] { 0, 129, 27, 218, 109, 214, 40, 213, 39, 83, 35, 5, 245, 134, 93, 184, 118, 152, 15, 14, 84, 66, 31, 197, 4, 147, 245, 94, 185, 7, 58, 196, 34, 48, 178, 223, 11, 177, 5, 90, 15, 236, 196, 110, 147, 243, 37, 205, 73, 137, 117, 56, 243, 76, 218, 182, 211, 202, 158, 93, 15, 180, 158, 233, 156, 92, 243, 50, 209, 211, 145, 102, 91, 198, 247, 217, 2, 205, 215, 71, 116, 103, 5, 18, 173, 34, 14, 106, 233, 27, 11, 83, 241, 180, 231, 182, 255, 76, 122, 251, 193, 73, 179, 85, 30, 63, 14, 200, 240, 69, 192, 25, 10, 186, 160, 145, 8, 167, 220, 203, 239, 190, 208, 182, 95, 123, 73, 213 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("712d4041-1e09-4380-8373-72385cbc5c9f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08") });

            migrationBuilder.CreateIndex(
                name: "UK_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MPFile_ProductId",
                table: "MPFile",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "UK_MPFile_FileName",
                table: "MPFile",
                column: "FileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "UK_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "UK_SubCategories_Name",
                table: "SubCategories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MPFile");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("712d4041-1e09-4380-8373-72385cbc5c9f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("107668af-ab09-4e5d-ba68-f7341dc7a372"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 195, 231, 117, 146, 162, 220, 141, 124, 208, 230, 71, 70, 144, 23, 71, 122, 229, 170, 252, 176, 10, 122, 12, 108, 89, 250, 38, 6, 105, 120, 48, 31, 166, 135, 220, 78, 251, 184, 221, 103, 201, 166, 185, 124, 86, 238, 248, 74, 52, 127, 192, 48, 61, 168, 47, 48, 21, 160, 171, 181, 80, 226, 250, 11 }, new byte[] { 59, 40, 78, 38, 161, 14, 18, 39, 238, 70, 148, 50, 57, 246, 88, 182, 152, 13, 77, 37, 188, 22, 209, 205, 141, 2, 169, 35, 98, 205, 53, 37, 57, 71, 191, 160, 194, 157, 142, 99, 128, 103, 113, 130, 196, 223, 27, 83, 239, 55, 5, 190, 127, 254, 66, 107, 0, 175, 169, 90, 18, 188, 43, 152, 138, 128, 183, 100, 100, 19, 62, 104, 123, 241, 139, 97, 134, 6, 165, 91, 175, 196, 149, 33, 253, 103, 214, 202, 178, 166, 65, 188, 24, 1, 234, 242, 231, 82, 9, 190, 102, 130, 10, 5, 108, 252, 201, 12, 240, 182, 102, 24, 170, 107, 193, 32, 191, 66, 220, 203, 243, 74, 4, 200, 173, 87, 210, 26 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("aa6bab7e-a949-4756-b869-4d9fb6e4598d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("107668af-ab09-4e5d-ba68-f7341dc7a372") });
        }
    }
}
