using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entidades;
using System.Collections.Generic;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        /* Creating DatabaseContext without Dependency Injection */
        public ApplicationContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(DatabaseConnection.ConnectionConfiguration
                                                    .GetConnectionString("DefaultConnection"));
            }
        }

        /* Creating DatabaseContext configured outside with Dependency Injection */
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
            //Cria a base de dados e suas respectivas tabelas se não existir.
            //Database.EnsureCreated();
            //Exclui a base de dados se existir.
            //Database.EnsureDeleted();            
            //Database.Log = instrucao => System.Diagnostics.Debug.WriteLine(instrucao);

            //PM > Add-Migration NewMigration - Project WAPP_ECommerce_BancoDados_V3
            //Microsoft.EntityFrameworkCore.Infrastructure[10403]
            //Entity Framework Core 2.2.6 - servicing - 10079 initialized 'BancoDadosCoreContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: MigrationsAssembly = WAPP_ECommerce_BancoDados_V3
            //To undo this action, use Remove-Migration.
            //PM >
            //Remove-Migration
            //Update-Database LastGoodMigration
            //Script-Migration 20190725054716_Add_new_tables
            //Add-Migration NewCampoPessoaConvenio

            //dotnet ef migrations add 20190725054716_Add_new_tables
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CategoriaItem> CategoriaItens { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCategoria> ProdutosCategorias { get; set; }
        public DbSet<ProdutoPreco> ProdutosPrecos { get; set; }
        public DbSet<TabelaPreco> TabelaPrecos { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
