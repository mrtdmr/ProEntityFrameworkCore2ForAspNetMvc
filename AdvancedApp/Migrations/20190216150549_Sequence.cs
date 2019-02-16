using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdvancedApp.Migrations
{
    public partial class Sequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ReferenceSequence",
                startValue: 100L,
                incrementBy: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 18, 5, 49, 727, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 17, 38, 3, 190, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "'REFERENCE_'+convert(varchar,next value for ReferenceSequence)",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ReferenceSequence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 17, 38, 3, 190, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 18, 5, 49, 727, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "'REFERENCE_'+convert(varchar,next value for ReferenceSequence)");
        }
    }
}
