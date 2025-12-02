using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerPlate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionVersionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PermissionVersion",
                table: "UsersEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionVersion",
                table: "UsersEntity");
        }
    }
}
