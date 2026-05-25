using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDashboard.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<string>(type: "TEXT", nullable: false),
                    TestType = table.Column<string>(type: "TEXT", nullable: false),
                    Passed = table.Column<bool>(type: "INTEGER", nullable: false),
                    MeasuredValue = table.Column<double>(type: "REAL", nullable: true),
                    ExpectedValue = table.Column<double>(type: "REAL", nullable: true),
                    Tolerance = table.Column<double>(type: "REAL", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    TestedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TechnicianName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResults");
        }
    }
}
