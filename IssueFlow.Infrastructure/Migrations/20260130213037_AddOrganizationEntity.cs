using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Profiles_OwnerProfileId",
                        column: x => x.OwnerProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_OrganizationId",
                table: "Profiles",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OwnerProfileId",
                table: "Organizations",
                column: "OwnerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Organizations_OrganizationId",
                table: "Profiles",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Organizations_OrganizationId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_OrganizationId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Profiles");
        }
    }
}
