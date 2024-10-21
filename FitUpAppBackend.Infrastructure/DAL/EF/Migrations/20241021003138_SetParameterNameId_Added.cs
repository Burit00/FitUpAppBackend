using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitUpAppBackend.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class SetParameterNameId_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetParameters_SetParameterNames_NameId",
                table: "SetParameters");

            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "SetParameters",
                newName: "SetParameterNameId");

            migrationBuilder.RenameIndex(
                name: "IX_SetParameters_NameId",
                table: "SetParameters",
                newName: "IX_SetParameters_SetParameterNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetParameters_SetParameterNames_SetParameterNameId",
                table: "SetParameters",
                column: "SetParameterNameId",
                principalTable: "SetParameterNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetParameters_SetParameterNames_SetParameterNameId",
                table: "SetParameters");

            migrationBuilder.RenameColumn(
                name: "SetParameterNameId",
                table: "SetParameters",
                newName: "NameId");

            migrationBuilder.RenameIndex(
                name: "IX_SetParameters_SetParameterNameId",
                table: "SetParameters",
                newName: "IX_SetParameters_NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetParameters_SetParameterNames_NameId",
                table: "SetParameters",
                column: "NameId",
                principalTable: "SetParameterNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
