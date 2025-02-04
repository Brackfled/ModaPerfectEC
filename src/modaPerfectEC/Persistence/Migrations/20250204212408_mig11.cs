using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("5218f916-c93b-42a5-b9b8-902d9e3b42e8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98472455-cbda-43ed-b1bd-13e37c197e15"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderPrice = table.Column<double>(type: "float", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInvoiceSended = table.Column<bool>(type: "bit", nullable: false),
                    IsUsdPrice = table.Column<bool>(type: "bit", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 519, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Admin", null },
                    { 520, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Read", null },
                    { 521, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Write", null },
                    { 522, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Create", null },
                    { 523, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Update", null },
                    { 524, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 102, 162, 239, 59, 67, 108, 117, 18, 35, 210, 72, 131, 191, 115, 71, 6, 254, 96, 223, 175, 123, 75, 121, 158, 224, 40, 73, 73, 126, 101, 210, 204, 233, 28, 237, 175, 217, 53, 96, 21, 14, 54, 247, 102, 3, 231, 13, 149, 33, 79, 49, 254, 159, 68, 27, 105, 122, 180, 72, 156, 246, 216, 175, 33 }, new byte[] { 8, 133, 54, 154, 186, 29, 172, 178, 34, 125, 74, 218, 177, 253, 136, 0, 186, 22, 232, 96, 157, 172, 114, 82, 92, 22, 101, 134, 57, 55, 139, 96, 218, 165, 19, 211, 250, 37, 238, 48, 108, 206, 8, 72, 83, 109, 202, 0, 92, 198, 110, 60, 97, 119, 224, 240, 139, 41, 148, 22, 204, 200, 214, 145, 16, 36, 137, 222, 249, 192, 82, 138, 172, 13, 134, 93, 115, 122, 186, 34, 231, 40, 225, 82, 232, 41, 79, 102, 15, 32, 90, 18, 127, 164, 196, 159, 126, 192, 192, 153, 63, 146, 125, 127, 236, 8, 95, 152, 255, 121, 252, 217, 52, 44, 68, 196, 158, 186, 110, 130, 253, 115, 159, 27, 150, 185, 11, 10 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("a5798b83-9fe3-49d1-b3e6-1c51bbbf331c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02") });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a5798b83-9fe3-49d1-b3e6-1c51bbbf331c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("98472455-cbda-43ed-b1bd-13e37c197e15"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 254, 164, 226, 228, 157, 74, 72, 158, 250, 170, 204, 192, 52, 219, 103, 65, 225, 106, 163, 135, 70, 102, 107, 210, 23, 58, 214, 134, 17, 163, 60, 45, 53, 90, 11, 139, 199, 182, 188, 221, 177, 197, 33, 159, 71, 196, 145, 67, 122, 146, 6, 110, 198, 44, 57, 195, 168, 255, 2, 236, 147, 11, 241, 83 }, new byte[] { 83, 42, 67, 114, 69, 52, 79, 210, 155, 198, 117, 148, 190, 240, 48, 186, 160, 82, 178, 123, 2, 216, 184, 212, 44, 82, 24, 202, 157, 206, 245, 211, 41, 247, 28, 181, 248, 89, 249, 142, 39, 10, 207, 155, 81, 90, 3, 118, 171, 149, 238, 122, 187, 65, 87, 37, 248, 10, 21, 48, 203, 106, 151, 138, 24, 233, 180, 123, 164, 150, 136, 14, 190, 17, 170, 71, 142, 167, 16, 249, 176, 172, 40, 19, 61, 230, 215, 61, 114, 160, 227, 177, 211, 125, 248, 126, 73, 239, 67, 52, 6, 125, 9, 7, 32, 221, 111, 48, 222, 53, 196, 52, 34, 246, 236, 166, 11, 60, 224, 97, 5, 26, 32, 237, 214, 209, 99, 216 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("5218f916-c93b-42a5-b9b8-902d9e3b42e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("98472455-cbda-43ed-b1bd-13e37c197e15") });
        }
    }
}
