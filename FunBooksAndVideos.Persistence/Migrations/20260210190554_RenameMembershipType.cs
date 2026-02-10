using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunBooksAndVideos.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameMembershipType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembershipTypes",
                table: "CustomerAccounts",
                newName: "MembershipType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembershipType",
                table: "CustomerAccounts",
                newName: "MembershipTypes");
        }
    }
}
