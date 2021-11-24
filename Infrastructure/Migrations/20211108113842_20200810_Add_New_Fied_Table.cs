using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class _20200810_Add_New_Fied_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TabelaPrecoId",
                table: "PedidoItens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_TabelaPrecoId",
                table: "PedidoItens",
                column: "TabelaPrecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItens_TabelaPrecos_TabelaPrecoId",
                table: "PedidoItens",
                column: "TabelaPrecoId",
                principalTable: "TabelaPrecos",
                principalColumn: "TabelaPrecoId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItens_TabelaPrecos_TabelaPrecoId",
                table: "PedidoItens");

            migrationBuilder.DropIndex(
                name: "IX_PedidoItens_TabelaPrecoId",
                table: "PedidoItens");

            migrationBuilder.DropColumn(
                name: "TabelaPrecoId",
                table: "PedidoItens");
        }
    }
}
