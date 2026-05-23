using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list.DAL.Migrations
{
    /// <inheritdoc />
    public partial class k : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLogged",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLogged",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
