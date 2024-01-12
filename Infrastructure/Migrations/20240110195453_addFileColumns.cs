using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addFileColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Employees",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EducationId", "Email", "Mobile", "Name" },
                values: new object[] { 1, null, "Ahmed@Gmail.Com", 12334, "Ahmed" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EducationId", "Email", "Mobile", "Name" },
                values: new object[] { 2, null, "Ali@Gmail.Com", 3435454, "ali" });
        }
    }
}
