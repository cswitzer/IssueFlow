using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IssueFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesIntoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0411f1d5-e108-457d-be8f-eee19f45af0b", "0411f1d5-e108-457d-be8f-eee19f45af0b", "Developer", "DEVELOPER" },
                    { "12345678-90ab-cdef-1234-567890abcdef", "12345678-90ab-cdef-1234-567890abcdef", "Support", "SUPPORT" },
                    { "50604dfe-6337-4583-80a3-eeb97833ebf7", "50604dfe-6337-4583-80a3-eeb97833ebf7", "Admin", "ADMIN" },
                    { "75079277-1507-4da0-8794-009c7efd8db1", "75079277-1507-4da0-8794-009c7efd8db1", "ProjectManager", "PROJECTMANAGER" },
                    { "a1b2c3d4-e5f6-7890-abcd-ef0123456789", "a1b2c3d4-e5f6-7890-abcd-ef0123456789", "Reporter", "REPORTER" },
                    { "d3b5f4c2-8f4e-4c3a-9f7e-2b6d5f4c2a1b", "d3b5f4c2-8f4e-4c3a-9f7e-2b6d5f4c2a1b", "Viewer", "VIEWER" },
                    { "e5db8e97-e573-44e5-bd65-95f69a04fabc", "e5db8e97-e573-44e5-bd65-95f69a04fabc", "Tester", "TESTER" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0411f1d5-e108-457d-be8f-eee19f45af0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12345678-90ab-cdef-1234-567890abcdef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50604dfe-6337-4583-80a3-eeb97833ebf7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75079277-1507-4da0-8794-009c7efd8db1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-7890-abcd-ef0123456789");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3b5f4c2-8f4e-4c3a-9f7e-2b6d5f4c2a1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5db8e97-e573-44e5-bd65-95f69a04fabc");

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssuePriorities",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueStatuses",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedAt",
                value: new DateTime(2026, 1, 16, 23, 53, 0, 581, DateTimeKind.Utc).AddTicks(3679));
        }
    }
}
