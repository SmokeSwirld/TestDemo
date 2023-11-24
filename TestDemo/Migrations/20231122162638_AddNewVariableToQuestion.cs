using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddNewVariableToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberQuestion",
                table: "Questionts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberQuestion",
                table: "Questionts");
        }
    }
}
