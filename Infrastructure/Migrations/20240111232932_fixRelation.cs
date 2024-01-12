using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class fixRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_EducationId",
                table: "Employees",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Educations_EducationId",
                table: "Employees",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Educations_EducationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EducationId",
                table: "Employees");
        }
    }
}
