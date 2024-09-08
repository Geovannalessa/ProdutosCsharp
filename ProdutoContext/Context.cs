using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProdutoContext
{
	public class Context : DbContext
	{
		private IConfiguration configuracao;
		public DbSet<ProdutoDomain.Entidades.Produtos> Produtos { get; set; }
		public Context(IConfiguration configuracao, DbContextOptions opcoes): base(opcoes)
		{
			this.configuracao = configuracao ?? throw new ArgumentNullException(nameof(configuracao));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var typeDataBase = configuracao["TypeDataBase"];
			var connectionString = configuracao.GetConnectionString(typeDataBase);

			if (typeDataBase == "SqlServer") {
				optionsBuilder.UseSqlServer(connectionString);
			}
		}
	}
}
