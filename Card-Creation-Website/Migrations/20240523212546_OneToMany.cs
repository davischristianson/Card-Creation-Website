using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Card_Creation_Website.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountUserId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountUserId",
                table: "Cards",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountUserId",
                table: "Cards",
                column: "AccountUserId",
                principalTable: "Accounts",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountUserId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountUserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Cards");
        }
    }
}
