using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("151760cd-279c-48e9-8a31-95bd55efedf5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[] { 527, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Users.GetFromAuth", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 139, 5, 18, 202, 62, 185, 244, 75, 154, 180, 157, 170, 155, 51, 19, 36, 62, 153, 182, 120, 203, 2, 158, 201, 4, 216, 185, 204, 226, 82, 170, 230, 172, 79, 173, 65, 50, 74, 237, 107, 249, 201, 121, 214, 34, 56, 153, 158, 246, 150, 103, 44, 77, 141, 190, 195, 232, 201, 193, 236, 123, 208, 39, 119 }, new byte[] { 200, 87, 211, 196, 91, 251, 128, 197, 15, 253, 171, 241, 187, 58, 145, 236, 122, 61, 23, 4, 176, 7, 37, 120, 181, 121, 224, 130, 160, 158, 89, 235, 9, 30, 47, 107, 201, 79, 7, 242, 0, 139, 166, 229, 18, 76, 2, 213, 32, 233, 20, 246, 82, 127, 166, 16, 102, 55, 224, 33, 125, 213, 13, 126, 26, 73, 197, 207, 103, 16, 147, 60, 237, 22, 59, 108, 91, 44, 142, 5, 79, 206, 210, 80, 61, 249, 111, 209, 122, 140, 45, 151, 192, 240, 37, 90, 76, 226, 199, 86, 129, 75, 40, 248, 180, 171, 129, 140, 164, 92, 0, 106, 85, 227, 255, 158, 13, 221, 122, 237, 171, 44, 30, 12, 135, 56, 36, 148 }, "Ben", "6666444555", "Konya VD", "HHH", null, 4 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("4ef56511-c4ff-4874-95e3-ceb39f8c0e5c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4ef56511-c4ff-4874-95e3-ceb39f8c0e5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 161, 124, 184, 253, 27, 13, 114, 115, 27, 107, 138, 16, 126, 74, 121, 70, 200, 238, 181, 104, 227, 16, 220, 180, 66, 185, 181, 118, 31, 214, 111, 31, 141, 219, 101, 95, 236, 172, 114, 128, 221, 62, 168, 111, 149, 73, 62, 245, 139, 156, 102, 46, 148, 31, 140, 118, 246, 112, 90, 122, 98, 2, 208, 90 }, new byte[] { 89, 163, 57, 138, 195, 240, 71, 135, 228, 171, 168, 49, 20, 176, 217, 119, 41, 25, 115, 106, 207, 127, 66, 92, 81, 9, 66, 208, 101, 80, 191, 94, 40, 92, 20, 179, 168, 62, 115, 10, 68, 112, 7, 119, 248, 135, 198, 56, 177, 138, 42, 253, 89, 247, 159, 124, 72, 5, 232, 100, 18, 140, 154, 0, 66, 19, 12, 149, 206, 146, 66, 170, 58, 133, 198, 55, 239, 48, 134, 235, 10, 212, 158, 150, 155, 69, 91, 58, 222, 19, 1, 192, 114, 175, 5, 250, 34, 255, 131, 37, 152, 133, 135, 35, 203, 53, 189, 169, 61, 246, 228, 142, 26, 218, 84, 139, 178, 162, 69, 110, 155, 149, 227, 210, 250, 83, 162, 82 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("151760cd-279c-48e9-8a31-95bd55efedf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("e9f2cde6-610e-4c24-92df-553632e5ef2c") });
        }
    }
}
