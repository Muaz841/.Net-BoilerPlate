using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerPlate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersEntity");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UsersEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersEntity",
                table: "UsersEntity");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UsersEntity");

            migrationBuilder.RenameTable(
                name: "UsersEntity",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
