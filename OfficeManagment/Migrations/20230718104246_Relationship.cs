using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeManagment.Migrations
{
    /// <inheritdoc />
    public partial class Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Positions_PositionId",
                table: "UserProjects");

            migrationBuilder.DropIndex(
                name: "IX_UserProjects_PositionId",
                table: "UserProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Positions_Id",
                table: "UserProjects",
                column: "Id",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Positions_Id",
                table: "UserProjects");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_PositionId",
                table: "UserProjects",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Positions_PositionId",
                table: "UserProjects",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
