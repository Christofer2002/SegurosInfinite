using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SegurosInfinite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coverages_insurances_InsuranceIdPoliza",
                table: "Coverages");

            migrationBuilder.DropForeignKey(
                name: "FK_insurances_Clients_ClientId",
                table: "insurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_insurances",
                table: "insurances");

            migrationBuilder.RenameTable(
                name: "insurances",
                newName: "Insurances");

            migrationBuilder.RenameIndex(
                name: "IX_insurances_ClientId",
                table: "Insurances",
                newName: "IX_Insurances_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances",
                column: "IdPoliza");

            migrationBuilder.AddForeignKey(
                name: "FK_Coverages_Insurances_InsuranceIdPoliza",
                table: "Coverages",
                column: "InsuranceIdPoliza",
                principalTable: "Insurances",
                principalColumn: "IdPoliza");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Clients_ClientId",
                table: "Insurances",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Cedula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coverages_Insurances_InsuranceIdPoliza",
                table: "Coverages");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Clients_ClientId",
                table: "Insurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances");

            migrationBuilder.RenameTable(
                name: "Insurances",
                newName: "insurances");

            migrationBuilder.RenameIndex(
                name: "IX_Insurances_ClientId",
                table: "insurances",
                newName: "IX_insurances_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_insurances",
                table: "insurances",
                column: "IdPoliza");

            migrationBuilder.AddForeignKey(
                name: "FK_Coverages_insurances_InsuranceIdPoliza",
                table: "Coverages",
                column: "InsuranceIdPoliza",
                principalTable: "insurances",
                principalColumn: "IdPoliza");

            migrationBuilder.AddForeignKey(
                name: "FK_insurances_Clients_ClientId",
                table: "insurances",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Cedula");
        }
    }
}
