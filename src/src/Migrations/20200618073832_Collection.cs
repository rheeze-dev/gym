using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropColumn(
                name: "Foods",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "Supplements",
                table: "Others");

            migrationBuilder.DropColumn(
                name: "Tshirts",
                table: "Others");

            migrationBuilder.RenameColumn(
                name: "Water",
                table: "Others",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "Others",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyCollection", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCollection");

            migrationBuilder.DropTable(
                name: "MonthlyCollection");

            migrationBuilder.DropColumn(
                name: "Items",
                table: "Others");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Others",
                newName: "Water");

            migrationBuilder.AddColumn<int>(
                name: "Foods",
                table: "Others",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Supplements",
                table: "Others",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tshirts",
                table: "Others",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Daily = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Monthly = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Yearly = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });
        }
    }
}
