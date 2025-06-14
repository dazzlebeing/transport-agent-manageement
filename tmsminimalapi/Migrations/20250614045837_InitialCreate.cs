using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tmsminimalapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "destination_city",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "source_city",
                table: "Bookings");

            migrationBuilder.AddColumn<Guid>(
                name: "destination_city_id",
                table: "Bookings",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "source_city_id",
                table: "Bookings",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_destination_city_id",
                table: "Bookings",
                column: "destination_city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_source_city_id",
                table: "Bookings",
                column: "source_city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_name",
                table: "Cities",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cities_destination_city_id",
                table: "Bookings",
                column: "destination_city_id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cities_source_city_id",
                table: "Bookings",
                column: "source_city_id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cities_destination_city_id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cities_source_city_id",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_destination_city_id",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_source_city_id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "destination_city_id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "source_city_id",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "destination_city",
                table: "Bookings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "source_city",
                table: "Bookings",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
