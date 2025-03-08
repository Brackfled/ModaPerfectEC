using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("224569e7-f567-47d3-b488-e8f6c380e665"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc19acc8-25c3-4433-8386-18376ac5c044"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductReturnId",
                table: "MPFile",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReturns_BasketItems_BasketItemId",
                        column: x => x.BasketItemId,
                        principalTable: "BasketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReturns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 528, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Admin", null },
                    { 529, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Read", null },
                    { 530, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Write", null },
                    { 531, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Create", null },
                    { 532, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Update", null },
                    { 533, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ProductReturns.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("f92e3b75-e962-436d-b8b8-33256d16df81"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 2, 202, 203, 115, 179, 253, 130, 208, 110, 36, 176, 137, 110, 57, 37, 14, 105, 26, 28, 254, 169, 158, 159, 221, 56, 137, 149, 247, 236, 97, 61, 170, 117, 96, 83, 169, 199, 152, 84, 7, 157, 23, 176, 108, 103, 101, 134, 219, 84, 105, 123, 109, 25, 66, 109, 73, 187, 84, 129, 253, 122, 187, 191, 74 }, new byte[] { 172, 176, 41, 182, 62, 144, 135, 103, 128, 143, 5, 84, 98, 97, 139, 63, 25, 112, 121, 207, 101, 20, 250, 129, 220, 107, 87, 78, 66, 101, 254, 152, 38, 184, 107, 147, 13, 107, 155, 64, 70, 4, 181, 148, 136, 217, 239, 36, 128, 217, 192, 34, 58, 16, 42, 87, 234, 198, 214, 178, 190, 144, 251, 30, 200, 205, 193, 20, 162, 29, 94, 177, 98, 234, 242, 94, 133, 108, 96, 127, 207, 233, 57, 142, 164, 1, 116, 189, 147, 4, 241, 76, 42, 161, 74, 80, 92, 135, 204, 7, 99, 240, 148, 152, 44, 124, 106, 29, 57, 74, 159, 201, 92, 242, 192, 40, 236, 162, 119, 91, 2, 152, 216, 28, 203, 158, 45, 142 }, "Ben", "6666444555", "Konya VD", "HHH", null, 4 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("9cdfeb67-e175-490c-8fe5-23b7374b1a2b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("f92e3b75-e962-436d-b8b8-33256d16df81") });

            migrationBuilder.CreateIndex(
                name: "IX_MPFile_ProductReturnId",
                table: "MPFile",
                column: "ProductReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturns_BasketItemId",
                table: "ProductReturns",
                column: "BasketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReturns_OrderId",
                table: "ProductReturns",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MPFile_ProductReturns_ProductReturnId",
                table: "MPFile",
                column: "ProductReturnId",
                principalTable: "ProductReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MPFile_ProductReturns_ProductReturnId",
                table: "MPFile");

            migrationBuilder.DropTable(
                name: "ProductReturns");

            migrationBuilder.DropIndex(
                name: "IX_MPFile_ProductReturnId",
                table: "MPFile");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9cdfeb67-e175-490c-8fe5-23b7374b1a2b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f92e3b75-e962-436d-b8b8-33256d16df81"));

            migrationBuilder.DropColumn(
                name: "ProductReturnId",
                table: "MPFile");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("bc19acc8-25c3-4433-8386-18376ac5c044"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 47, 31, 64, 42, 80, 53, 9, 192, 202, 157, 4, 88, 242, 149, 27, 47, 46, 203, 123, 18, 251, 12, 16, 82, 198, 50, 190, 33, 143, 105, 204, 122, 129, 208, 175, 240, 187, 250, 197, 190, 172, 60, 201, 204, 171, 113, 65, 83, 170, 240, 44, 91, 225, 91, 138, 173, 19, 0, 212, 223, 88, 188, 86, 215 }, new byte[] { 156, 5, 9, 222, 215, 202, 209, 255, 228, 100, 246, 216, 107, 77, 169, 100, 199, 170, 221, 16, 124, 242, 34, 231, 218, 53, 138, 87, 33, 133, 197, 111, 201, 54, 56, 14, 185, 62, 207, 14, 159, 1, 55, 26, 71, 47, 84, 0, 26, 178, 241, 173, 123, 81, 126, 143, 70, 127, 146, 4, 180, 165, 209, 24, 51, 163, 103, 58, 127, 109, 211, 11, 240, 22, 224, 80, 225, 102, 72, 95, 149, 190, 204, 251, 227, 133, 152, 15, 104, 174, 253, 12, 136, 149, 156, 159, 142, 86, 212, 250, 233, 31, 207, 135, 149, 153, 68, 223, 87, 244, 191, 195, 92, 89, 89, 84, 239, 71, 214, 186, 127, 191, 24, 225, 41, 95, 109, 151 }, "Ben", "6666444555", "Konya VD", "HHH", null, 4 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("224569e7-f567-47d3-b488-e8f6c380e665"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("bc19acc8-25c3-4433-8386-18376ac5c044") });
        }
    }
}
