using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdvancedApp.Migrations
{
    public partial class RestrictDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 15, 52, 59, 221, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 15, 41, 52, 896, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity",
                columns: new[] { "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName" },
                principalTable: "Employees",
                principalColumns: new[] { "SSN", "FirstName", "FamilyName" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 16, 15, 41, 52, 896, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 16, 15, 52, 59, 221, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryIdentity_Employees_PrimarySSN_PrimaryFirstName_PrimaryFamilyName",
                table: "SecondaryIdentity",
                columns: new[] { "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName" },
                principalTable: "Employees",
                principalColumns: new[] { "SSN", "FirstName", "FamilyName" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
