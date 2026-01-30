using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueNumberToIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueNumber",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueNumber",
                table: "Issues");
        }
    }
}
