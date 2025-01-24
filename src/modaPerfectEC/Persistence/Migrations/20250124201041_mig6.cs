using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_SubCategoryId",
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
                values: new object[] { new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 196, 187, 59, 180, 165, 162, 175, 54, 50, 66, 51, 1, 227, 174, 102, 23, 40, 62, 213, 232, 9, 223, 231, 123, 7, 136, 224, 244, 199, 39, 245, 40, 222, 228, 202, 32, 195, 91, 135, 222, 138, 37, 65, 68, 1, 194, 225, 150, 99, 111, 45, 7, 241, 73, 164, 133, 184, 16, 47, 200, 206, 161, 45, 65 }, new byte[] { 38, 218, 149, 117, 239, 247, 149, 193, 45, 67, 245, 239, 139, 252, 211, 238, 232, 70, 126, 235, 115, 27, 213, 62, 137, 81, 139, 196, 221, 38, 80, 219, 115, 133, 61, 59, 83, 248, 104, 200, 198, 6, 229, 216, 56, 121, 2, 139, 79, 14, 35, 187, 170, 73, 112, 137, 176, 81, 89, 104, 123, 144, 202, 252, 49, 94, 3, 184, 168, 135, 162, 10, 167, 32, 238, 240, 131, 76, 36, 119, 137, 98, 210, 104, 102, 252, 234, 152, 119, 190, 4, 48, 218, 197, 192, 181, 217, 55, 251, 244, 88, 42, 110, 168, 221, 121, 158, 242, 19, 170, 78, 106, 114, 30, 88, 55, 41, 180, 198, 203, 161, 228, 15, 9, 133, 123, 104, 48 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("60627686-30f1-4813-b5a2-79b0c7d09e0a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9") });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("60627686-30f1-4813-b5a2-79b0c7d09e0a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("99f0fa98-e15f-4a66-8040-caf74ab63ed9"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("b1008f5a-46be-4e67-9c3e-60c00cfffde2"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 35, 23, 40, 74, 96, 117, 173, 45, 223, 35, 153, 225, 68, 28, 145, 56, 85, 197, 58, 203, 76, 40, 235, 188, 152, 35, 5, 160, 227, 136, 73, 133, 176, 192, 4, 196, 167, 34, 245, 52, 18, 210, 90, 161, 176, 175, 65, 6, 214, 192, 151, 29, 110, 165, 75, 62, 240, 76, 126, 40, 95, 182, 95, 226 }, new byte[] { 152, 110, 16, 182, 82, 149, 87, 51, 117, 9, 229, 253, 166, 135, 169, 255, 104, 177, 153, 47, 27, 160, 129, 70, 82, 164, 249, 35, 176, 48, 226, 134, 243, 118, 6, 253, 78, 3, 103, 207, 53, 112, 181, 35, 215, 68, 211, 74, 60, 191, 143, 254, 132, 51, 99, 61, 131, 206, 192, 107, 228, 24, 22, 183, 174, 52, 149, 188, 225, 191, 92, 158, 182, 46, 193, 183, 59, 124, 165, 240, 37, 74, 209, 164, 4, 30, 235, 85, 254, 224, 117, 16, 181, 25, 21, 213, 78, 254, 84, 30, 212, 51, 215, 74, 92, 195, 223, 109, 127, 77, 159, 123, 45, 24, 223, 54, 242, 16, 56, 113, 116, 252, 213, 4, 179, 178, 126, 226 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("8cbf9fcc-1f5c-42ae-b460-28aac7c90e31"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("b1008f5a-46be-4e67-9c3e-60c00cfffde2") });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
