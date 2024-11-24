using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProdutoDomain.Entidades;

namespace ProdutoContext
{
    public class Context : DbContext
    {
        #region configuração do banco de dados
        private IConfiguration configuracao;

        public DbSet<Produtos> Produtos { get; set; }
		public DbSet<Usuario> Usuario { get; set; }
		public Context(IConfiguration configuracao, DbContextOptions<Context> opcoes) : base(opcoes)
        {
            this.configuracao = configuracao ?? throw new ArgumentNullException(nameof(configuracao));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDataBase = configuracao["TypeDataBase"];
            var connectionString = configuracao.GetConnectionString(typeDataBase);

            if (typeDataBase == "SqlServer")
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        #endregion
    }
}
