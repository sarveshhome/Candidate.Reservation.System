using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidate.System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CandidateName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    SelectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "ReservationConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ReservationPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectionResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CutoffMark = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false),
                    SelectionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Category",
                table: "Candidates",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Marks",
                table: "Candidates",
                column: "Marks");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Timestamp",
                table: "Candidates",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationConfigs_Category",
                table: "ReservationConfigs",
                column: "Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectionResults_CandidateId",
                table: "SelectionResults",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionResults_Category",
                table: "SelectionResults",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_SelectionResults_ProcessedAt",
                table: "SelectionResults",
                column: "ProcessedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "ReservationConfigs");

            migrationBuilder.DropTable(
                name: "SelectionResults");
        }
    }
}
