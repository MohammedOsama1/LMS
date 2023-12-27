using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.EF.Migrations
{
    /// <inheritdoc />
    public partial class edit_date_Time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReisteredData",
                table: "Borrowers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReisteredData",
                table: "Borrowers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
