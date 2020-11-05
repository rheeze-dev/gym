using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LockerNumber",
                table: "Members",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MedicalCondition",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Members",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LockerNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MedicalCondition",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Members");
        }
    }
}
