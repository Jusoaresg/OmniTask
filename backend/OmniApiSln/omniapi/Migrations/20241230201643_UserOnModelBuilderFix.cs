using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace omniapi.Migrations
{
    /// <inheritdoc />
    public partial class UserOnModelBuilderFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_User_UserId_UserEmail",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_UserId_UserEmail",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Invoice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_User_UserId",
                table: "Invoice",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_User_UserId",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_UserId",
                table: "Invoice");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Invoice",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                columns: new[] { "Id", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_UserId_UserEmail",
                table: "Invoice",
                columns: new[] { "UserId", "UserEmail" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_User_UserId_UserEmail",
                table: "Invoice",
                columns: new[] { "UserId", "UserEmail" },
                principalTable: "User",
                principalColumns: new[] { "Id", "Email" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
