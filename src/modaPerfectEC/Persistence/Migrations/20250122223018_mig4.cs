using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9e22f8ab-3b8b-4b6c-9b97-fb675fd7dab3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 504, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MPFiles.Create", null },
                    { 505, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MPFiles.Delete", null },
                    { 506, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MPFiles.Read", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("eb7130eb-5b88-4574-82a1-4f44c0351b9d"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 131, 173, 255, 246, 34, 161, 119, 80, 60, 19, 155, 105, 242, 184, 24, 171, 17, 93, 39, 11, 245, 28, 27, 114, 178, 15, 175, 92, 5, 123, 162, 151, 155, 86, 81, 242, 36, 85, 54, 4, 240, 49, 191, 211, 208, 237, 164, 9, 231, 188, 54, 76, 216, 240, 187, 225, 136, 62, 177, 114, 56, 144, 153, 0 }, new byte[] { 93, 19, 52, 225, 179, 30, 12, 75, 143, 113, 195, 94, 45, 98, 4, 73, 64, 50, 167, 29, 54, 93, 54, 255, 42, 213, 221, 228, 149, 244, 252, 84, 83, 9, 37, 199, 59, 251, 145, 53, 137, 129, 200, 169, 213, 210, 122, 83, 237, 26, 50, 200, 43, 154, 18, 12, 97, 137, 42, 150, 17, 199, 131, 44, 91, 84, 232, 58, 37, 24, 198, 23, 64, 34, 92, 46, 121, 43, 196, 231, 232, 184, 2, 97, 220, 75, 61, 38, 233, 247, 148, 42, 46, 56, 247, 153, 30, 121, 172, 92, 193, 157, 108, 150, 206, 105, 6, 224, 15, 127, 229, 61, 127, 173, 49, 6, 53, 239, 5, 158, 127, 100, 75, 61, 119, 30, 85, 216 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("197678e1-a429-45f1-a520-ed67e64c4060"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("eb7130eb-5b88-4574-82a1-4f44c0351b9d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("197678e1-a429-45f1-a520-ed67e64c4060"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("eb7130eb-5b88-4574-82a1-4f44c0351b9d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 126, 135, 197, 181, 167, 167, 56, 131, 127, 57, 43, 160, 176, 246, 171, 248, 74, 249, 114, 194, 159, 101, 1, 5, 165, 123, 227, 237, 244, 171, 89, 187, 154, 84, 101, 126, 27, 108, 194, 107, 33, 5, 19, 49, 77, 52, 93, 104, 133, 26, 162, 67, 110, 79, 88, 104, 6, 68, 25, 111, 251, 73, 98, 127 }, new byte[] { 151, 233, 40, 28, 42, 152, 72, 152, 133, 250, 130, 42, 206, 156, 213, 9, 251, 184, 81, 147, 125, 184, 196, 70, 69, 171, 117, 7, 169, 102, 251, 49, 165, 231, 82, 53, 121, 174, 40, 226, 145, 138, 187, 244, 27, 196, 28, 149, 226, 80, 144, 33, 92, 93, 9, 105, 251, 142, 250, 128, 210, 67, 89, 96, 38, 39, 111, 143, 185, 175, 51, 206, 239, 57, 54, 149, 81, 241, 218, 116, 118, 12, 55, 57, 62, 85, 104, 23, 135, 179, 2, 191, 112, 38, 209, 86, 192, 87, 179, 216, 166, 176, 187, 31, 159, 33, 115, 169, 45, 242, 211, 178, 125, 111, 145, 142, 112, 122, 105, 206, 244, 171, 45, 159, 107, 231, 60, 165 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("9e22f8ab-3b8b-4b6c-9b97-fb675fd7dab3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("d7805a6b-0682-4c15-add3-582cc9e0567a") });
        }
    }
}
