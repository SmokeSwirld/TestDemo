using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionts",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTest = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionTest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerBool1 = table.Column<bool>(type: "bit", nullable: false),
                    AnswerBool2 = table.Column<bool>(type: "bit", nullable: false),
                    AnswerBool3 = table.Column<bool>(type: "bit", nullable: false),
                    AnswerBool4 = table.Column<bool>(type: "bit", nullable: false),
                    AnswerBool5 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionts", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameTest = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questionts");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
