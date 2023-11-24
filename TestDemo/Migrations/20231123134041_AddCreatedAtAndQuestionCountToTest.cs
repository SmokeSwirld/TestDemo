using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtAndQuestionCountToTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "Tests");
        }
    }
}
