using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class Quantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalEquipments",
                table: "Equipments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Equipments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Equipments");

            migrationBuilder.AlterColumn<int>(
                name: "TotalEquipments",
                table: "Equipments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
