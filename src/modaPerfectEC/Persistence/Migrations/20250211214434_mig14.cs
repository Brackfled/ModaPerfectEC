using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4ef56511-c4ff-4874-95e3-ceb39f8c0e5c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6"));

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "MPFile",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<string>(
                name: "CollectionName",
                table: "MPFile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionState",
                table: "MPFile",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("bc19acc8-25c3-4433-8386-18376ac5c044"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 47, 31, 64, 42, 80, 53, 9, 192, 202, 157, 4, 88, 242, 149, 27, 47, 46, 203, 123, 18, 251, 12, 16, 82, 198, 50, 190, 33, 143, 105, 204, 122, 129, 208, 175, 240, 187, 250, 197, 190, 172, 60, 201, 204, 171, 113, 65, 83, 170, 240, 44, 91, 225, 91, 138, 173, 19, 0, 212, 223, 88, 188, 86, 215 }, new byte[] { 156, 5, 9, 222, 215, 202, 209, 255, 228, 100, 246, 216, 107, 77, 169, 100, 199, 170, 221, 16, 124, 242, 34, 231, 218, 53, 138, 87, 33, 133, 197, 111, 201, 54, 56, 14, 185, 62, 207, 14, 159, 1, 55, 26, 71, 47, 84, 0, 26, 178, 241, 173, 123, 81, 126, 143, 70, 127, 146, 4, 180, 165, 209, 24, 51, 163, 103, 58, 127, 109, 211, 11, 240, 22, 224, 80, 225, 102, 72, 95, 149, 190, 204, 251, 227, 133, 152, 15, 104, 174, 253, 12, 136, 149, 156, 159, 142, 86, 212, 250, 233, 31, 207, 135, 149, 153, 68, 223, 87, 244, 191, 195, 92, 89, 89, 84, 239, 71, 214, 186, 127, 191, 24, 225, 41, 95, 109, 151 }, "Ben", "6666444555", "Konya VD", "HHH", null, 4 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("224569e7-f567-47d3-b488-e8f6c380e665"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("bc19acc8-25c3-4433-8386-18376ac5c044") });

            migrationBuilder.CreateIndex(
                name: "IX_MPFile_CollectionName",
                table: "MPFile",
                column: "CollectionName",
                unique: true,
                filter: "[CollectionName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MPFile_CollectionName",
                table: "MPFile");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("224569e7-f567-47d3-b488-e8f6c380e665"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc19acc8-25c3-4433-8386-18376ac5c044"));

            migrationBuilder.DropColumn(
                name: "CollectionName",
                table: "MPFile");

            migrationBuilder.DropColumn(
                name: "CollectionState",
                table: "MPFile");

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "MPFile",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 139, 5, 18, 202, 62, 185, 244, 75, 154, 180, 157, 170, 155, 51, 19, 36, 62, 153, 182, 120, 203, 2, 158, 201, 4, 216, 185, 204, 226, 82, 170, 230, 172, 79, 173, 65, 50, 74, 237, 107, 249, 201, 121, 214, 34, 56, 153, 158, 246, 150, 103, 44, 77, 141, 190, 195, 232, 201, 193, 236, 123, 208, 39, 119 }, new byte[] { 200, 87, 211, 196, 91, 251, 128, 197, 15, 253, 171, 241, 187, 58, 145, 236, 122, 61, 23, 4, 176, 7, 37, 120, 181, 121, 224, 130, 160, 158, 89, 235, 9, 30, 47, 107, 201, 79, 7, 242, 0, 139, 166, 229, 18, 76, 2, 213, 32, 233, 20, 246, 82, 127, 166, 16, 102, 55, 224, 33, 125, 213, 13, 126, 26, 73, 197, 207, 103, 16, 147, 60, 237, 22, 59, 108, 91, 44, 142, 5, 79, 206, 210, 80, 61, 249, 111, 209, 122, 140, 45, 151, 192, 240, 37, 90, 76, 226, 199, 86, 129, 75, 40, 248, 180, 171, 129, 140, 164, 92, 0, 106, 85, 227, 255, 158, 13, 221, 122, 237, 171, 44, 30, 12, 135, 56, 36, 148 }, "Ben", "6666444555", "Konya VD", "HHH", null, 4 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("4ef56511-c4ff-4874-95e3-ceb39f8c0e5c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("f099f6d7-2879-4f89-8029-7bbb6ebef1e6") });
        }
    }
}
