using Microsoft.EntityFrameworkCore.Migrations;

namespace BmsSurvey.Persistence.Migrations
{
    public partial class SurveyPageSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageSize",
                table: "Surveys",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageSize",
                table: "Surveys");
        }
    }
}
