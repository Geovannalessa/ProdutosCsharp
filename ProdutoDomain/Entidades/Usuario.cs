using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoDomain.Entidades
{
	public class Usuario
	{
		public int Id { get; set; }
		public string nome { get; set; }
		public string email { get; set; }
		public string senha { get; set;}
		public int cep { get; set; }
		public string cpf { get; set; }
	}
}
