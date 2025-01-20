using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("712d4041-1e09-4380-8373-72385cbc5c9f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08"));

            migrationBuilder.AddColumn<string>(
                name: "Hex",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 126, 135, 197, 181, 167, 167, 56, 131, 127, 57, 43, 160, 176, 246, 171, 248, 74, 249, 114, 194, 159, 101, 1, 5, 165, 123, 227, 237, 244, 171, 89, 187, 154, 84, 101, 126, 27, 108, 194, 107, 33, 5, 19, 49, 77, 52, 93, 104, 133, 26, 162, 67, 110, 79, 88, 104, 6, 68, 25, 111, 251, 73, 98, 127 }, new byte[] { 151, 233, 40, 28, 42, 152, 72, 152, 133, 250, 130, 42, 206, 156, 213, 9, 251, 184, 81, 147, 125, 184, 196, 70, 69, 171, 117, 7, 169, 102, 251, 49, 165, 231, 82, 53, 121, 174, 40, 226, 145, 138, 187, 244, 27, 196, 28, 149, 226, 80, 144, 33, 92, 93, 9, 105, 251, 142, 250, 128, 210, 67, 89, 96, 38, 39, 111, 143, 185, 175, 51, 206, 239, 57, 54, 149, 81, 241, 218, 116, 118, 12, 55, 57, 62, 85, 104, 23, 135, 179, 2, 191, 112, 38, 209, 86, 192, 87, 179, 216, 166, 176, 187, 31, 159, 33, 115, 169, 45, 242, 211, 178, 125, 111, 145, 142, 112, 122, 105, 206, 244, 171, 45, 159, 107, 231, 60, 165 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("9e22f8ab-3b8b-4b6c-9b97-fb675fd7dab3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9e22f8ab-3b8b-4b6c-9b97-fb675fd7dab3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a"));

            migrationBuilder.DropColumn(
                name: "Hex",
                table: "ProductVariants");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 223, 104, 176, 222, 234, 236, 58, 71, 145, 50, 244, 136, 88, 103, 83, 169, 79, 132, 179, 151, 21, 35, 5, 19, 195, 152, 136, 100, 171, 63, 10, 12, 196, 2, 198, 55, 59, 175, 205, 7, 167, 193, 127, 139, 107, 178, 37, 152, 75, 233, 167, 155, 234, 69, 216, 195, 74, 16, 121, 2, 187, 171, 132, 167 }, new byte[] { 0, 129, 27, 218, 109, 214, 40, 213, 39, 83, 35, 5, 245, 134, 93, 184, 118, 152, 15, 14, 84, 66, 31, 197, 4, 147, 245, 94, 185, 7, 58, 196, 34, 48, 178, 223, 11, 177, 5, 90, 15, 236, 196, 110, 147, 243, 37, 205, 73, 137, 117, 56, 243, 76, 218, 182, 211, 202, 158, 93, 15, 180, 158, 233, 156, 92, 243, 50, 209, 211, 145, 102, 91, 198, 247, 217, 2, 205, 215, 71, 116, 103, 5, 18, 173, 34, 14, 106, 233, 27, 11, 83, 241, 180, 231, 182, 255, 76, 122, 251, 193, 73, 179, 85, 30, 63, 14, 200, 240, 69, 192, 25, 10, 186, 160, 145, 8, 167, 220, 203, 239, 190, 208, 182, 95, 123, 73, 213 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("712d4041-1e09-4380-8373-72385cbc5c9f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("a4e47536-dfb8-42eb-8e17-6cde3acf7a08") });
        }
    }
}
