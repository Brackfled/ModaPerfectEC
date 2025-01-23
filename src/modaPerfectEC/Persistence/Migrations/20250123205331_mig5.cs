using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

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
                values: new object[] { new Guid("b1008f5a-46be-4e67-9c3e-60c00cfffde2"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 35, 23, 40, 74, 96, 117, 173, 45, 223, 35, 153, 225, 68, 28, 145, 56, 85, 197, 58, 203, 76, 40, 235, 188, 152, 35, 5, 160, 227, 136, 73, 133, 176, 192, 4, 196, 167, 34, 245, 52, 18, 210, 90, 161, 176, 175, 65, 6, 214, 192, 151, 29, 110, 165, 75, 62, 240, 76, 126, 40, 95, 182, 95, 226 }, new byte[] { 152, 110, 16, 182, 82, 149, 87, 51, 117, 9, 229, 253, 166, 135, 169, 255, 104, 177, 153, 47, 27, 160, 129, 70, 82, 164, 249, 35, 176, 48, 226, 134, 243, 118, 6, 253, 78, 3, 103, 207, 53, 112, 181, 35, 215, 68, 211, 74, 60, 191, 143, 254, 132, 51, 99, 61, 131, 206, 192, 107, 228, 24, 22, 183, 174, 52, 149, 188, 225, 191, 92, 158, 182, 46, 193, 183, 59, 124, 165, 240, 37, 74, 209, 164, 4, 30, 235, 85, 254, 224, 117, 16, 181, 25, 21, 213, 78, 254, 84, 30, 212, 51, 215, 74, 92, 195, 223, 109, 127, 77, 159, 123, 45, 24, 223, 54, 242, 16, 56, 113, 116, 252, 213, 4, 179, 178, 126, 226 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("8cbf9fcc-1f5c-42ae-b460-28aac7c90e31"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("b1008f5a-46be-4e67-9c3e-60c00cfffde2") });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("8cbf9fcc-1f5c-42ae-b460-28aac7c90e31"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b1008f5a-46be-4e67-9c3e-60c00cfffde2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("eb7130eb-5b88-4574-82a1-4f44c0351b9d"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 131, 173, 255, 246, 34, 161, 119, 80, 60, 19, 155, 105, 242, 184, 24, 171, 17, 93, 39, 11, 245, 28, 27, 114, 178, 15, 175, 92, 5, 123, 162, 151, 155, 86, 81, 242, 36, 85, 54, 4, 240, 49, 191, 211, 208, 237, 164, 9, 231, 188, 54, 76, 216, 240, 187, 225, 136, 62, 177, 114, 56, 144, 153, 0 }, new byte[] { 93, 19, 52, 225, 179, 30, 12, 75, 143, 113, 195, 94, 45, 98, 4, 73, 64, 50, 167, 29, 54, 93, 54, 255, 42, 213, 221, 228, 149, 244, 252, 84, 83, 9, 37, 199, 59, 251, 145, 53, 137, 129, 200, 169, 213, 210, 122, 83, 237, 26, 50, 200, 43, 154, 18, 12, 97, 137, 42, 150, 17, 199, 131, 44, 91, 84, 232, 58, 37, 24, 198, 23, 64, 34, 92, 46, 121, 43, 196, 231, 232, 184, 2, 97, 220, 75, 61, 38, 233, 247, 148, 42, 46, 56, 247, 153, 30, 121, 172, 92, 193, 157, 108, 150, 206, 105, 6, 224, 15, 127, 229, 61, 127, 173, 49, 6, 53, 239, 5, 158, 127, 100, 75, 61, 119, 30, 85, 216 }, "Ben", "6666444555", "Konya VD", "HHH", null, 0 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("197678e1-a429-45f1-a520-ed67e64c4060"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("eb7130eb-5b88-4574-82a1-4f44c0351b9d") });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
