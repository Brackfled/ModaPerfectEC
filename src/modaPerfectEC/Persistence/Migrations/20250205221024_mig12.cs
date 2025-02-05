using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("a5798b83-9fe3-49d1-b3e6-1c51bbbf331c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 525, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.GetById", null },
                    { 526, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Orders.GetListFromAuth", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 161, 124, 184, 253, 27, 13, 114, 115, 27, 107, 138, 16, 126, 74, 121, 70, 200, 238, 181, 104, 227, 16, 220, 180, 66, 185, 181, 118, 31, 214, 111, 31, 141, 219, 101, 95, 236, 172, 114, 128, 221, 62, 168, 111, 149, 73, 62, 245, 139, 156, 102, 46, 148, 31, 140, 118, 246, 112, 90, 122, 98, 2, 208, 90 }, new byte[] { 89, 163, 57, 138, 195, 240, 71, 135, 228, 171, 168, 49, 20, 176, 217, 119, 41, 25, 115, 106, 207, 127, 66, 92, 81, 9, 66, 208, 101, 80, 191, 94, 40, 92, 20, 179, 168, 62, 115, 10, 68, 112, 7, 119, 248, 135, 198, 56, 177, 138, 42, 253, 89, 247, 159, 124, 72, 5, 232, 100, 18, 140, 154, 0, 66, 19, 12, 149, 206, 146, 66, 170, 58, 133, 198, 55, 239, 48, 134, 235, 10, 212, 158, 150, 155, 69, 91, 58, 222, 19, 1, 192, 114, 175, 5, 250, 34, 255, 131, 37, 152, 133, 135, 35, 203, 53, 189, 169, 61, 246, 228, 142, 26, 218, 84, 139, 178, 162, 69, 110, 155, 149, 227, 210, 250, 83, 162, 82 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("151760cd-279c-48e9-8a31-95bd55efedf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("151760cd-279c-48e9-8a31-95bd55efedf5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 102, 162, 239, 59, 67, 108, 117, 18, 35, 210, 72, 131, 191, 115, 71, 6, 254, 96, 223, 175, 123, 75, 121, 158, 224, 40, 73, 73, 126, 101, 210, 204, 233, 28, 237, 175, 217, 53, 96, 21, 14, 54, 247, 102, 3, 231, 13, 149, 33, 79, 49, 254, 159, 68, 27, 105, 122, 180, 72, 156, 246, 216, 175, 33 }, new byte[] { 8, 133, 54, 154, 186, 29, 172, 178, 34, 125, 74, 218, 177, 253, 136, 0, 186, 22, 232, 96, 157, 172, 114, 82, 92, 22, 101, 134, 57, 55, 139, 96, 218, 165, 19, 211, 250, 37, 238, 48, 108, 206, 8, 72, 83, 109, 202, 0, 92, 198, 110, 60, 97, 119, 224, 240, 139, 41, 148, 22, 204, 200, 214, 145, 16, 36, 137, 222, 249, 192, 82, 138, 172, 13, 134, 93, 115, 122, 186, 34, 231, 40, 225, 82, 232, 41, 79, 102, 15, 32, 90, 18, 127, 164, 196, 159, 126, 192, 192, 153, 63, 146, 125, 127, 236, 8, 95, 152, 255, 121, 252, 217, 52, 44, 68, 196, 158, 186, 110, 130, 253, 115, 159, 27, 150, 185, 11, 10 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("a5798b83-9fe3-49d1-b3e6-1c51bbbf331c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("71f5c21f-c9d6-4c0e-9846-9164de986e02") });
        }
    }
}
