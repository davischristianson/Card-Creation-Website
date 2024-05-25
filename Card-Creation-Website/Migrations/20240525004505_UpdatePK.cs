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
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountUserId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountUserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cards",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Accounts",
                newName: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_AccountId",
                table: "Cards",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Accounts_AccountId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_AccountId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Cards",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "UserId");

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
    }
}
