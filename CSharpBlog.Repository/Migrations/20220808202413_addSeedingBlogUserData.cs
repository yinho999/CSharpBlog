#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpBlog.Repository.Migrations
{
    public partial class addSeedingBlogUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password", "Username" },
                values: new object[,]
                {
                    { new Guid("354fa244-3c83-438b-9f2b-284707fd3935"), "asd@gmail.com", "User", "123456", "user" },
                    { new Guid("354fa244-3c83-438b-9f2b-284707fd3936"), "asdf@gmail.com", "User1", "123456", "user1" }
                });

            migrationBuilder.InsertData(
                table: "Blog",
                columns: new[] { "Id", "Content", "Created", "LastModified", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"), "C# Blog", new DateTime(2022, 8, 8, 20, 24, 13, 840, DateTimeKind.Utc).AddTicks(9230), new DateTime(2022, 8, 8, 20, 24, 13, 840, DateTimeKind.Utc).AddTicks(9230), "C# Blog", new Guid("354fa244-3c83-438b-9f2b-284707fd3936") },
                    { new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"), "C# Blog 2", new DateTime(2022, 8, 8, 20, 24, 13, 840, DateTimeKind.Utc).AddTicks(9230), new DateTime(2022, 8, 8, 20, 24, 13, 840, DateTimeKind.Utc).AddTicks(9230), "C# Blog 2", new Guid("354fa244-3c83-438b-9f2b-284707fd3935") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60da"));

            migrationBuilder.DeleteData(
                table: "Blog",
                keyColumn: "Id",
                keyValue: new Guid("fd6c9dc8-97af-4ad5-a361-a79e9b2b60db"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3935"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("354fa244-3c83-438b-9f2b-284707fd3936"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");
        }
    }
}
