using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueAssignerAndAssignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeProfileId",
                table: "Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReporterProfileId",
                table: "Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeProfileId",
                table: "Issues",
                column: "AssigneeProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterProfileId",
                table: "Issues",
                column: "ReporterProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Profiles_AssigneeProfileId",
                table: "Issues",
                column: "AssigneeProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Profiles_ReporterProfileId",
                table: "Issues",
                column: "ReporterProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Profiles_AssigneeProfileId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Profiles_ReporterProfileId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeProfileId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ReporterProfileId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeProfileId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ReporterProfileId",
                table: "Issues");
        }
    }
}
