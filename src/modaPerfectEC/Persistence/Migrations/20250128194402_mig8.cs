using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("1aecf973-8893-45ec-952b-f541a9f59a64"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084"));

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    IsOrderBasket = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductVariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductAmount = table.Column<int>(type: "int", nullable: false),
                    RemainingAfterDelivery = table.Column<int>(type: "int", nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 507, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Admin", null },
                    { 508, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Read", null },
                    { 509, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Write", null },
                    { 510, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Create", null },
                    { 511, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Update", null },
                    { 512, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Baskets.Delete", null },
                    { 513, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Admin", null },
                    { 514, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Read", null },
                    { 515, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Write", null },
                    { 516, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Create", null },
                    { 517, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Update", null },
                    { 518, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BasketItems.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 70, 81, 160, 194, 35, 116, 145, 160, 143, 8, 11, 193, 91, 120, 130, 231, 72, 244, 23, 162, 17, 236, 219, 198, 66, 143, 241, 226, 18, 30, 105, 219, 151, 166, 11, 117, 180, 152, 244, 247, 12, 188, 213, 7, 104, 9, 35, 80, 242, 163, 222, 229, 116, 223, 63, 41, 161, 18, 72, 11, 151, 8, 35, 71 }, new byte[] { 189, 191, 109, 223, 113, 64, 244, 162, 39, 112, 134, 55, 146, 130, 185, 168, 209, 155, 124, 198, 9, 169, 231, 201, 205, 47, 115, 137, 212, 107, 161, 176, 110, 66, 5, 196, 68, 194, 37, 215, 250, 60, 56, 228, 56, 207, 32, 73, 75, 158, 188, 237, 186, 253, 229, 91, 25, 170, 130, 78, 234, 214, 38, 141, 206, 198, 72, 197, 209, 130, 218, 63, 96, 206, 48, 36, 240, 229, 72, 104, 100, 89, 241, 212, 235, 234, 2, 133, 183, 155, 6, 96, 181, 175, 194, 213, 207, 202, 19, 229, 213, 117, 60, 23, 150, 141, 124, 38, 242, 122, 120, 250, 55, 215, 94, 83, 215, 168, 18, 195, 48, 152, 197, 100, 88, 94, 100, 232 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("b77ec252-a47d-4fc0-9e18-50a83b24d956"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57") });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductVariantId",
                table: "BasketItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b77ec252-a47d-4fc0-9e18-50a83b24d956"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 219, 96, 67, 237, 253, 21, 66, 131, 147, 130, 150, 131, 128, 86, 114, 69, 50, 182, 201, 52, 25, 237, 29, 202, 163, 83, 57, 224, 14, 100, 196, 194, 208, 87, 22, 129, 171, 43, 184, 104, 26, 159, 155, 245, 216, 152, 146, 215, 79, 65, 209, 232, 254, 185, 225, 255, 146, 208, 39, 96, 152, 20, 196, 137 }, new byte[] { 44, 56, 214, 80, 110, 61, 93, 51, 134, 126, 133, 237, 5, 45, 102, 191, 146, 215, 151, 138, 20, 30, 131, 80, 142, 208, 94, 205, 58, 143, 84, 102, 80, 104, 109, 7, 65, 87, 54, 146, 137, 111, 78, 135, 223, 161, 251, 161, 134, 23, 173, 112, 113, 91, 58, 86, 245, 60, 89, 201, 177, 196, 211, 104, 57, 122, 146, 88, 190, 214, 119, 50, 67, 232, 211, 208, 190, 123, 40, 235, 70, 63, 200, 121, 213, 255, 4, 28, 88, 220, 188, 243, 39, 65, 82, 34, 167, 108, 198, 122, 253, 153, 77, 73, 167, 224, 67, 22, 98, 25, 219, 81, 114, 192, 204, 142, 6, 53, 194, 91, 157, 38, 12, 185, 121, 158, 173, 184 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("1aecf973-8893-45ec-952b-f541a9f59a64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084") });
        }
    }
}
