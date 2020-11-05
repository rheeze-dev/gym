using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class Dailycollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTimeout",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "DailyCollection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "DailyCollection",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "DailyCollection",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DailyCollection");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "DailyCollection");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "DailyCollection");

            migrationBuilder.AddColumn<bool>(
                name: "IsTimeout",
                table: "Logins",
                nullable: false,
                defaultValue: false);
        }
    }
}
