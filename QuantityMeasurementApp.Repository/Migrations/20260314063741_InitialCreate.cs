using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuantityMeasurementApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationType = table.Column<int>(type: "integer", nullable: false),
                    MeasurementCategory = table.Column<int>(type: "integer", nullable: false),
                    Operand1Value = table.Column<double>(type: "double precision", nullable: false),
                    Operand1Unit = table.Column<string>(type: "text", nullable: false),
                    Operand2Value = table.Column<double>(type: "double precision", nullable: false),
                    Operand2Unit = table.Column<string>(type: "text", nullable: false),
                    ResultValue = table.Column<double>(type: "double precision", nullable: false),
                    ResultUnit = table.Column<string>(type: "text", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Measurements");
        }
    }
}
