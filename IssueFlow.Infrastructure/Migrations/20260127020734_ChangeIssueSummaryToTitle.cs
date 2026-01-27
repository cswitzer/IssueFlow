using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIssueSummaryToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Issues",
                newName: "Title");

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 27, 2, 7, 34, 311, DateTimeKind.Utc).AddTicks(5326));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Issues",
                newName: "Summary");

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 21, 0, 20, 32, 260, DateTimeKind.Utc).AddTicks(8546));
        }
    }
}
