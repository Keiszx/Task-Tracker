using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Task_Tracker_2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    TitleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_titles", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "toDos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toDos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_toDos_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_toDos_titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "titles",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "statuses",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "closed", "Done" },
                    { "open", "To Do" }
                });

            migrationBuilder.InsertData(
                table: "titles",
                columns: new[] { "TitleId", "Name" },
                values: new object[,]
                {
                    { "gala", "Gala" },
                    { "home", "Home" },
                    { "school", "School" },
                    { "videogames", "Video Games" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_toDos_StatusId",
                table: "toDos",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_toDos_TitleId",
                table: "toDos",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "toDos");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "titles");
        }
    }
}
