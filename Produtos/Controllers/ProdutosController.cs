using Microsoft.AspNetCore.Mvc;
using ProdutoContext;

namespace Produtos.Controllers
{
	public class ProdutosController : Controller
	{
		private readonly Context _context;

		public ProdutosController(Context context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public IActionResult Produtos()
		{
			var produtos = _context.Produtos.ToList();
			return View(produtos);
		}
	}
}
