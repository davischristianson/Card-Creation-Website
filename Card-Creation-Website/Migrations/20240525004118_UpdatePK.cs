using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Card_Creation_Website.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cards",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Accounts",
                newName: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Cards",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "UserId");
        }
    }
}
