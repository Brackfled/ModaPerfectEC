using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2fb95536-ac12-40de-a109-b068f32ea404"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a130f194-a78a-4373-957d-7ef8752cafbb"));

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("98472455-cbda-43ed-b1bd-13e37c197e15"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 254, 164, 226, 228, 157, 74, 72, 158, 250, 170, 204, 192, 52, 219, 103, 65, 225, 106, 163, 135, 70, 102, 107, 210, 23, 58, 214, 134, 17, 163, 60, 45, 53, 90, 11, 139, 199, 182, 188, 221, 177, 197, 33, 159, 71, 196, 145, 67, 122, 146, 6, 110, 198, 44, 57, 195, 168, 255, 2, 236, 147, 11, 241, 83 }, new byte[] { 83, 42, 67, 114, 69, 52, 79, 210, 155, 198, 117, 148, 190, 240, 48, 186, 160, 82, 178, 123, 2, 216, 184, 212, 44, 82, 24, 202, 157, 206, 245, 211, 41, 247, 28, 181, 248, 89, 249, 142, 39, 10, 207, 155, 81, 90, 3, 118, 171, 149, 238, 122, 187, 65, 87, 37, 248, 10, 21, 48, 203, 106, 151, 138, 24, 233, 180, 123, 164, 150, 136, 14, 190, 17, 170, 71, 142, 167, 16, 249, 176, 172, 40, 19, 61, 230, 215, 61, 114, 160, 227, 177, 211, 125, 248, 126, 73, 239, 67, 52, 6, 125, 9, 7, 32, 221, 111, 48, 222, 53, 196, 52, 34, 246, 236, 166, 11, 60, 224, 97, 5, 26, 32, 237, 214, 209, 99, 216 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("5218f916-c93b-42a5-b9b8-902d9e3b42e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("98472455-cbda-43ed-b1bd-13e37c197e15") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("5218f916-c93b-42a5-b9b8-902d9e3b42e8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("98472455-cbda-43ed-b1bd-13e37c197e15"));

            migrationBuilder.AlterColumn<string>(
                name: "District",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AuthenticatorType", "CarrierCompanyInfo", "City", "Country", "CreatedDate", "CustomerCode", "DeletedDate", "District", "Email", "FirstName", "GsmNumber", "IdentityHash", "IdentitySalt", "LastName", "PasswordHash", "PasswordSalt", "Reference", "TaxNumber", "TaxOffice", "TradeName", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("a130f194-a78a-4373-957d-7ef8752cafbb"), "Dere", 0, null, "Konya", "Türkiye", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Meram", "oncellhsyn@outlook.com", "Hüseyin", "05555555555", null, null, "ÖNCEL", new byte[] { 101, 39, 183, 72, 70, 162, 148, 198, 154, 12, 177, 34, 203, 138, 28, 230, 202, 199, 242, 37, 84, 36, 140, 201, 156, 52, 61, 22, 2, 179, 130, 188, 26, 43, 252, 152, 99, 61, 70, 94, 136, 76, 179, 70, 142, 87, 35, 144, 59, 158, 101, 138, 165, 225, 227, 54, 30, 212, 72, 237, 93, 175, 87, 253 }, new byte[] { 211, 216, 155, 177, 90, 202, 197, 23, 219, 244, 69, 137, 162, 65, 232, 35, 175, 171, 35, 132, 13, 151, 240, 196, 202, 246, 185, 49, 248, 196, 142, 143, 0, 202, 242, 63, 14, 230, 217, 197, 245, 180, 34, 47, 64, 172, 157, 99, 165, 19, 39, 183, 221, 106, 87, 205, 35, 96, 53, 235, 202, 80, 72, 44, 206, 236, 72, 0, 116, 234, 185, 236, 192, 80, 99, 24, 169, 107, 236, 86, 3, 173, 123, 15, 142, 244, 46, 238, 169, 167, 165, 197, 140, 155, 44, 137, 32, 41, 129, 222, 234, 37, 253, 202, 149, 222, 139, 215, 59, 106, 79, 56, 138, 130, 164, 10, 247, 124, 128, 134, 158, 8, 214, 241, 184, 195, 54, 222 }, "Ben", "6666444555", "Konya VD", "HHH", null, 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2fb95536-ac12-40de-a109-b068f32ea404"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 456, null, new Guid("a130f194-a78a-4373-957d-7ef8752cafbb") });
        }
    }
}
