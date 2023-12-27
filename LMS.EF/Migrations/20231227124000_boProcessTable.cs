using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.EF.Migrations
{
    /// <inheritdoc />
    public partial class boProcessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "BorrowingRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "BorrowingRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowingRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "BorrowingRecords");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "BorrowingRecords");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BorrowingRecords");
        }
    }
}
