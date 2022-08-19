using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpBlog.Repository.Migrations
{
    public partial class ChangedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4320c03b-abe9-4424-a278-9cbb1b6f4224"),
                column: "ConcurrencyStamp",
                value: "9dce9e28-5d04-43d1-9e8b-4427748eabd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed82663d-f917-4a6e-8f42-c51dbab9af72"),
                column: "ConcurrencyStamp",
                value: "8297b1d8-05a3-406d-ac2d-4fa53d5f1bc4");

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 17, 19, 34, 11, 803, DateTimeKind.Utc).AddTicks(4150), new DateTime(2022, 8, 17, 19, 34, 11, 803, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 17, 19, 34, 11, 803, DateTimeKind.Utc).AddTicks(4160), new DateTime(2022, 8, 17, 19, 34, 11, 803, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3935"),
                column: "ConcurrencyStamp",
                value: "9488d6c6-6365-4fbe-a65b-875fa82ad4b0");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3936"),
                column: "ConcurrencyStamp",
                value: "ec1abbe8-e48f-4a9c-b254-a6866a299ba0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4320c03b-abe9-4424-a278-9cbb1b6f4224"),
                column: "ConcurrencyStamp",
                value: "9d670581-dd09-44a0-9763-77878fd66991");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed82663d-f917-4a6e-8f42-c51dbab9af72"),
                column: "ConcurrencyStamp",
                value: "2a27c70f-2bf9-4d0c-9266-8564992013ab");

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 13, 14, 35, 57, 97, DateTimeKind.Utc).AddTicks(9790), new DateTime(2022, 8, 13, 14, 35, 57, 97, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 13, 14, 35, 57, 97, DateTimeKind.Utc).AddTicks(9790), new DateTime(2022, 8, 13, 14, 35, 57, 97, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3935"),
                column: "ConcurrencyStamp",
                value: "a2431e3e-23dd-4618-8aaa-1e67b1447ac2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3936"),
                column: "ConcurrencyStamp",
                value: "c0c0c6db-5ec6-4f1e-9b6a-f5b3120c6ab1");
        }
    }
}
