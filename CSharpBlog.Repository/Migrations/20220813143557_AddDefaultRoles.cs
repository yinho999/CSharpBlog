using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpBlog.Repository.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4320c03b-abe9-4424-a278-9cbb1b6f4224"), "9d670581-dd09-44a0-9763-77878fd66991", "Administrator", "ADMINISTRATOR" },
                    { new Guid("ed82663d-f917-4a6e-8f42-c51dbab9af72"), "2a27c70f-2bf9-4d0c-9266-8564992013ab", "User", "USER" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4320c03b-abe9-4424-a278-9cbb1b6f4224"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed82663d-f917-4a6e-8f42-c51dbab9af72"));

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 13, 13, 23, 9, 526, DateTimeKind.Utc).AddTicks(7550), new DateTime(2022, 8, 13, 13, 23, 9, 526, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2022, 8, 13, 13, 23, 9, 526, DateTimeKind.Utc).AddTicks(7560), new DateTime(2022, 8, 13, 13, 23, 9, 526, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3935"),
                column: "ConcurrencyStamp",
                value: "0a27044a-b480-4a60-a940-551176b835cd");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3936"),
                column: "ConcurrencyStamp",
                value: "6f0806a5-825a-43c1-ba94-ef66052bc622");
        }
    }
}
