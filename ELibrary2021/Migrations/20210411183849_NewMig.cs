using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary2021.Migrations
{
    public partial class NewMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookAuthor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookParts = table.Column<int>(type: "int", nullable: false),
                    BookClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookLang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Returner",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    takerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    period = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Takers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TakerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookIdid = table.Column<int>(type: "int", nullable: true),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    periodInDays = table.Column<int>(type: "int", nullable: false),
                    TakedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Takers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Takers_Books_BookIdid",
                        column: x => x.BookIdid,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Takers_BookIdid",
                table: "Takers",
                column: "BookIdid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Returner");

            migrationBuilder.DropTable(
                name: "Takers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
