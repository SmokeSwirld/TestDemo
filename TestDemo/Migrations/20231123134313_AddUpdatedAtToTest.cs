using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtToTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tests",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tests");
        }
    }
}
