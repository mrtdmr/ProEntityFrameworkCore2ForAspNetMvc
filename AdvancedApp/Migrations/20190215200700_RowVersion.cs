using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdvancedApp.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 15, 23, 7, 0, 773, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 15, 22, 39, 2, 248, DateTimeKind.Local));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Employees",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 15, 22, 39, 2, 248, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 15, 23, 7, 0, 773, DateTimeKind.Local));
        }
    }
}
