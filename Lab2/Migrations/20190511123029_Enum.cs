using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class Enum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Watched",
                table: "Movies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Movies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Watched",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Movies",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
