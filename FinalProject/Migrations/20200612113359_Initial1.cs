using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    eventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    eventName = table.Column<string>(type: "TEXT", nullable: true),
                    eventDescription = table.Column<string>(type: "TEXT", nullable: true),
                    eventDate = table.Column<string>(type: "TEXT", nullable: true),
                    streetLocation = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.eventId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    personId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    lastName = table.Column<string>(type: "TEXT", nullable: true),
                    phoneNumber = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.personId);
                });

            migrationBuilder.CreateTable(
                name: "AttendEvent",
                columns: table => new
                {
                    eventId = table.Column<int>(type: "INTEGER", nullable: false),
                    personId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendEvent", x => new { x.eventId, x.personId });
                    table.ForeignKey(
                        name: "FK_AttendEvent_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "eventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendEvent_Persons_personId",
                        column: x => x.personId,
                        principalTable: "Persons",
                        principalColumn: "personId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendEvent_personId",
                table: "AttendEvent",
                column: "personId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendEvent");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
