using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beauty_Salon.Data.Migrations
{
    public partial class migration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationTerm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTerm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TreatmentID = table.Column<int>(type: "int", nullable: true),
                    ReservationTermID = table.Column<int>(type: "int", nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservation_ReservationTerm",
                        column: x => x.ReservationTermID,
                        principalTable: "ReservationTerm",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Reservation_Threatment",
                        column: x => x.TreatmentID,
                        principalTable: "Treatment",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservationTermID",
                table: "Reservation",
                column: "ReservationTermID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TreatmentID",
                table: "Reservation",
                column: "TreatmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "ReservationTerm");

            migrationBuilder.DropTable(
                name: "Treatment");
        }
    }
}
