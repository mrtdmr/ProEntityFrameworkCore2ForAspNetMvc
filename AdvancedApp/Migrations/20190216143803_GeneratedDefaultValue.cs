using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdvancedApp.Migrations
{
    public partial class GeneratedDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 17, 38, 3, 190, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 15, 52, 59, 221, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedValue",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 15, 52, 59, 221, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 17, 38, 3, 190, DateTimeKind.Local));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Employees",
                rowVersion: true,
                nullable: true);
        }
    }
}
