using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yarukoto.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamePrimaryKeyForWorkspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Workspaces",
                newName: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "Workspaces",
                newName: "Id");
        }
    }
}
