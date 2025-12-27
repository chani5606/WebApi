using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate90 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Gifts_IdGiftsId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Purchasers_IdPurchaserId",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Categories_CatgoryId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Donors_DonorId",
                table: "Gifts");

            migrationBuilder.RenameColumn(
                name: "DonorId",
                table: "Gifts",
                newName: "IdDonor");

            migrationBuilder.RenameColumn(
                name: "CatgoryId",
                table: "Gifts",
                newName: "IdCatgory");

            migrationBuilder.RenameIndex(
                name: "IX_Gifts_DonorId",
                table: "Gifts",
                newName: "IX_Gifts_IdDonor");

            migrationBuilder.RenameIndex(
                name: "IX_Gifts_CatgoryId",
                table: "Gifts",
                newName: "IX_Gifts_IdCatgory");

            migrationBuilder.RenameColumn(
                name: "IdPurchaserId",
                table: "Basket",
                newName: "IdPurchaser");

            migrationBuilder.RenameColumn(
                name: "IdGiftsId",
                table: "Basket",
                newName: "IdGifts");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_IdPurchaserId",
                table: "Basket",
                newName: "IX_Basket_IdPurchaser");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_IdGiftsId",
                table: "Basket",
                newName: "IX_Basket_IdGifts");

            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Gifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Gifts_IdGifts",
                table: "Basket",
                column: "IdGifts",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Purchasers_IdPurchaser",
                table: "Basket",
                column: "IdPurchaser",
                principalTable: "Purchasers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Categories_IdCatgory",
                table: "Gifts",
                column: "IdCatgory",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Donors_IdDonor",
                table: "Gifts",
                column: "IdDonor",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Gifts_IdGifts",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Basket_Purchasers_IdPurchaser",
                table: "Basket");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Categories_IdCatgory",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Donors_IdDonor",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Gifts");

            migrationBuilder.RenameColumn(
                name: "IdDonor",
                table: "Gifts",
                newName: "DonorId");

            migrationBuilder.RenameColumn(
                name: "IdCatgory",
                table: "Gifts",
                newName: "CatgoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Gifts_IdDonor",
                table: "Gifts",
                newName: "IX_Gifts_DonorId");

            migrationBuilder.RenameIndex(
                name: "IX_Gifts_IdCatgory",
                table: "Gifts",
                newName: "IX_Gifts_CatgoryId");

            migrationBuilder.RenameColumn(
                name: "IdPurchaser",
                table: "Basket",
                newName: "IdPurchaserId");

            migrationBuilder.RenameColumn(
                name: "IdGifts",
                table: "Basket",
                newName: "IdGiftsId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_IdPurchaser",
                table: "Basket",
                newName: "IX_Basket_IdPurchaserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_IdGifts",
                table: "Basket",
                newName: "IX_Basket_IdGiftsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Gifts_IdGiftsId",
                table: "Basket",
                column: "IdGiftsId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_Purchasers_IdPurchaserId",
                table: "Basket",
                column: "IdPurchaserId",
                principalTable: "Purchasers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Categories_CatgoryId",
                table: "Gifts",
                column: "CatgoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Donors_DonorId",
                table: "Gifts",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
