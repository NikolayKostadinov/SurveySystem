using Microsoft.EntityFrameworkCore.Migrations;

namespace BmsSurvey.Persistence.Migrations
{
    public partial class FreeTextAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FreeTextAnswerAnswer_Value",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeTextAnswerAnswer_Value",
                table: "Answers");
        }
    }
}
