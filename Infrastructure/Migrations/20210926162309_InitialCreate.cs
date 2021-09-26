using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "TabelaPrecos",
                columns: table => new
                {
                    TabelaPrecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPrecos", x => x.TabelaPrecoId);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NomeReduzido = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.UnidadeId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaItens",
                columns: table => new
                {
                    CategoriaItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaItens", x => x.CategoriaItemId);
                    table.ForeignKey(
                        name: "FK_CategoriaItens_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosPrecos",
                columns: table => new
                {
                    ProdutoPrecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoCusto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TabelaPrecoId = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosPrecos", x => x.ProdutoPrecoId);
                    table.ForeignKey(
                        name: "FK_ProdutosPrecos_TabelaPrecos_TabelaPrecoId",
                        column: x => x.TabelaPrecoId,
                        principalTable: "TabelaPrecos",
                        principalColumn: "TabelaPrecoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosPrecos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "UnidadeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NomeReduzido = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ProdutoPrecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_ProdutosPrecos_ProdutoPrecoId",
                        column: x => x.ProdutoPrecoId,
                        principalTable: "ProdutosPrecos",
                        principalColumn: "ProdutoPrecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosCategorias",
                columns: table => new
                {
                    ProdutoCategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaItemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosCategorias", x => x.ProdutoCategoriaId);
                    table.ForeignKey(
                        name: "FK_ProdutosCategorias_CategoriaItens_CategoriaItemId",
                        column: x => x.CategoriaItemId,
                        principalTable: "CategoriaItens",
                        principalColumn: "CategoriaItemId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProdutosCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProdutosCategorias_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaItens_CategoriaId",
                table: "CategoriaItens",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProdutoPrecoId",
                table: "Produtos",
                column: "ProdutoPrecoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosCategorias_CategoriaId",
                table: "ProdutosCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosCategorias_CategoriaItemId",
                table: "ProdutosCategorias",
                column: "CategoriaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosCategorias_ProdutoId",
                table: "ProdutosCategorias",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosPrecos_TabelaPrecoId",
                table: "ProdutosPrecos",
                column: "TabelaPrecoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosPrecos_UnidadeId",
                table: "ProdutosPrecos",
                column: "UnidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutosCategorias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CategoriaItens");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ProdutosPrecos");

            migrationBuilder.DropTable(
                name: "TabelaPrecos");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
