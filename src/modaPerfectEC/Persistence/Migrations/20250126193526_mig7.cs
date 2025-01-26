using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("60627686-30f1-4813-b5a2-79b0c7d09e0a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9"));

            migrationBuilder.AddColumn<double>(
                name: "PriceUSD",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 219, 96, 67, 237, 253, 21, 66, 131, 147, 130, 150, 131, 128, 86, 114, 69, 50, 182, 201, 52, 25, 237, 29, 202, 163, 83, 57, 224, 14, 100, 196, 194, 208, 87, 22, 129, 171, 43, 184, 104, 26, 159, 155, 245, 216, 152, 146, 215, 79, 65, 209, 232, 254, 185, 225, 255, 146, 208, 39, 96, 152, 20, 196, 137 }, new byte[] { 44, 56, 214, 80, 110, 61, 93, 51, 134, 126, 133, 237, 5, 45, 102, 191, 146, 215, 151, 138, 20, 30, 131, 80, 142, 208, 94, 205, 58, 143, 84, 102, 80, 104, 109, 7, 65, 87, 54, 146, 137, 111, 78, 135, 223, 161, 251, 161, 134, 23, 173, 112, 113, 91, 58, 86, 245, 60, 89, 201, 177, 196, 211, 104, 57, 122, 146, 88, 190, 214, 119, 50, 67, 232, 211, 208, 190, 123, 40, 235, 70, 63, 200, 121, 213, 255, 4, 28, 88, 220, 188, 243, 39, 65, 82, 34, 167, 108, 198, 122, 253, 153, 77, 73, 167, 224, 67, 22, 98, 25, 219, 81, 114, 192, 204, 142, 6, 53, 194, 91, 157, 38, 12, 185, 121, 158, 173, 184 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("1aecf973-8893-45ec-952b-f541a9f59a64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("1aecf973-8893-45ec-952b-f541a9f59a64"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9807ed72-cf90-4b1a-892b-6f963bda8084"));

            migrationBuilder.DropColumn(
                name: "PriceUSD",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 196, 187, 59, 180, 165, 162, 175, 54, 50, 66, 51, 1, 227, 174, 102, 23, 40, 62, 213, 232, 9, 223, 231, 123, 7, 136, 224, 244, 199, 39, 245, 40, 222, 228, 202, 32, 195, 91, 135, 222, 138, 37, 65, 68, 1, 194, 225, 150, 99, 111, 45, 7, 241, 73, 164, 133, 184, 16, 47, 200, 206, 161, 45, 65 }, new byte[] { 38, 218, 149, 117, 239, 247, 149, 193, 45, 67, 245, 239, 139, 252, 211, 238, 232, 70, 126, 235, 115, 27, 213, 62, 137, 81, 139, 196, 221, 38, 80, 219, 115, 133, 61, 59, 83, 248, 104, 200, 198, 6, 229, 216, 56, 121, 2, 139, 79, 14, 35, 187, 170, 73, 112, 137, 176, 81, 89, 104, 123, 144, 202, 252, 49, 94, 3, 184, 168, 135, 162, 10, 167, 32, 238, 240, 131, 76, 36, 119, 137, 98, 210, 104, 102, 252, 234, 152, 119, 190, 4, 48, 218, 197, 192, 181, 217, 55, 251, 244, 88, 42, 110, 168, 221, 121, 158, 242, 19, 170, 78, 106, 114, 30, 88, 55, 41, 180, 198, 203, 161, 228, 15, 9, 133, 123, 104, 48 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("60627686-30f1-4813-b5a2-79b0c7d09e0a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9") });
        }
    }
}
