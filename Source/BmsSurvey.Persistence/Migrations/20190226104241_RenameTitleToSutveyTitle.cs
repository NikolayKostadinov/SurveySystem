using Microsoft.EntityFrameworkCore.Migrations;

namespace BmsSurvey.Persistence.Migrations
{
    public partial class RenameTitleToSutveyTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Surveys",
                newName: "SurveyTitle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurveyTitle",
                table: "Surveys",
                newName: "Title");
        }
    }
}
