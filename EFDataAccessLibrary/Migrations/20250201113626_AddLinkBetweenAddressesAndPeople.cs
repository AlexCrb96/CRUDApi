using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkBetweenAddressesAndPeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_People_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_People_PersonId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Addresses");
        }
    }
}
