using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("b77ec252-a47d-4fc0-9e18-50a83b24d956"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57"));

            migrationBuilder.AddColumn<double>(
                name: "TotalPriceUSD",
                table: "Baskets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("a130f194-a78a-4373-957d-7ef8752cafbb"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 101, 39, 183, 72, 70, 162, 148, 198, 154, 12, 177, 34, 203, 138, 28, 230, 202, 199, 242, 37, 84, 36, 140, 201, 156, 52, 61, 22, 2, 179, 130, 188, 26, 43, 252, 152, 99, 61, 70, 94, 136, 76, 179, 70, 142, 87, 35, 144, 59, 158, 101, 138, 165, 225, 227, 54, 30, 212, 72, 237, 93, 175, 87, 253 }, new byte[] { 211, 216, 155, 177, 90, 202, 197, 23, 219, 244, 69, 137, 162, 65, 232, 35, 175, 171, 35, 132, 13, 151, 240, 196, 202, 246, 185, 49, 248, 196, 142, 143, 0, 202, 242, 63, 14, 230, 217, 197, 245, 180, 34, 47, 64, 172, 157, 99, 165, 19, 39, 183, 221, 106, 87, 205, 35, 96, 53, 235, 202, 80, 72, 44, 206, 236, 72, 0, 116, 234, 185, 236, 192, 80, 99, 24, 169, 107, 236, 86, 3, 173, 123, 15, 142, 244, 46, 238, 169, 167, 165, 197, 140, 155, 44, 137, 32, 41, 129, 222, 234, 37, 253, 202, 149, 222, 139, 215, 59, 106, 79, 56, 138, 130, 164, 10, 247, 124, 128, 134, 158, 8, 214, 241, 184, 195, 54, 222 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2fb95536-ac12-40de-a109-b068f32ea404"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("a130f194-a78a-4373-957d-7ef8752cafbb") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2fb95536-ac12-40de-a109-b068f32ea404"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a130f194-a78a-4373-957d-7ef8752cafbb"));

            migrationBuilder.DropColumn(
                name: "TotalPriceUSD",
                table: "Baskets");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 70, 81, 160, 194, 35, 116, 145, 160, 143, 8, 11, 193, 91, 120, 130, 231, 72, 244, 23, 162, 17, 236, 219, 198, 66, 143, 241, 226, 18, 30, 105, 219, 151, 166, 11, 117, 180, 152, 244, 247, 12, 188, 213, 7, 104, 9, 35, 80, 242, 163, 222, 229, 116, 223, 63, 41, 161, 18, 72, 11, 151, 8, 35, 71 }, new byte[] { 189, 191, 109, 223, 113, 64, 244, 162, 39, 112, 134, 55, 146, 130, 185, 168, 209, 155, 124, 198, 9, 169, 231, 201, 205, 47, 115, 137, 212, 107, 161, 176, 110, 66, 5, 196, 68, 194, 37, 215, 250, 60, 56, 228, 56, 207, 32, 73, 75, 158, 188, 237, 186, 253, 229, 91, 25, 170, 130, 78, 234, 214, 38, 141, 206, 198, 72, 197, 209, 130, 218, 63, 96, 206, 48, 36, 240, 229, 72, 104, 100, 89, 241, 212, 235, 234, 2, 133, 183, 155, 6, 96, 181, 175, 194, 213, 207, 202, 19, 229, 213, 117, 60, 23, 150, 141, 124, 38, 242, 122, 120, 250, 55, 215, 94, 83, 215, 168, 18, 195, 48, 152, 197, 100, 88, 94, 100, 232 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("b77ec252-a47d-4fc0-9e18-50a83b24d956"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("d77a1a23-7e72-4f78-9769-aba78089ed57") });
        }
    }
}
