using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class CreateManyToManyRelationBetweenPersonsAndAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AddressPerson",
                columns: table => new
                {
                    AddressesId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPerson", x => new { x.AddressesId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_AddressPerson_Addresses_AddressesId",
                        column: x => x.AddressesId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressPerson_People_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressPerson_PersonsId",
                table: "AddressPerson",
                column: "PersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressPerson");

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
    }
}
