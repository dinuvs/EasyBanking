using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyBanking.DataAccess.Migrations
{
    public partial class AddTransactionTypetoTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeId",
                table: "Transaction",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionType_TransactionTypeId",
                table: "Transaction",
                column: "TransactionTypeId",
                principalTable: "TransactionType",
                principalColumn: "TransactionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionType_TransactionTypeId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TransactionTypeId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Transaction");
        }
    }
}
