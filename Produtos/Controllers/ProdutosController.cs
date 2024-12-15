using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using ProdutoContext;
using ProdutoDomain.Entidades;
using System.Drawing;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using static Produtos.Controllers.Criptografia;
namespace Produtos.Controllers
{
    public class ProdutosController : Controller
	{
		private readonly CriptografiaService _criptografiaService;
		private readonly Context _context;
		public ProdutosController(Context context, CriptografiaService criptografiaService)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_criptografiaService = criptografiaService;
		}

		#region login
		public IActionResult Login()
        {
            return View();
        }
        public IActionResult EntrarConta(string email, string senha)
        {
            //buscando o usuario pelo email no BD
            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.email == email) ?? throw new Exception("Dados inseridos incorretos!");
            string senhaCriptografado = _criptografiaService.HashSenha(senha);
			if (usuario.senha != senhaCriptografado) throw new Exception("Dados inseridos incorretos!");
			
			return RedirectToAction("Index", "Home");
		}
        public IActionResult AlterarSenha()
        {
            return View();
        }
		public IActionResult VerificaUsuario(string email, string cpf, string senha, string senha2)
        {
            try
            {
			    Usuario usuarioBD = _context.Usuario.FirstOrDefault(u => u.email == email) ?? throw new Exception("Usuário não encontrado ou não existe!");
			    if (usuarioBD.cpf != cpf) throw new Exception("Usuário não encontrado ou não existe!");
                if (senha != senha2) throw new Exception("As senhas não se coincidem!");
			    usuarioBD.senha = _criptografiaService.HashSenha(senha);
				this.SalvarUsuarioBD(usuarioBD);
                return RedirectToAction("Login", "Produtos");

            }
            catch (Exception ex)
            {

                throw;
            }
		}
		
		#endregion
		

        #region views
        public IActionResult Produtos()
		{
			var produtos = _context.Produtos.ToList();
			return View(produtos);
		}
        public IActionResult CriarEditarProduto(int? id = null)
        {
            if (id == null) {
                ProdutoDomain.Entidades.Produtos produtoNovo = new ProdutoDomain.Entidades.Produtos();
                return View(produtoNovo);
            }
            else if (id.HasValue)
            {
                ProdutoDomain.Entidades.Produtos produtoBD = this.ObtemProduto(id.Value);
                return View(produtoBD);
            }
            return View();
        }
        public IActionResult ExcluirProduto(int id) {
            try
            {
                ProdutoDomain.Entidades.Produtos produto = this.ObtemProduto(id);
                this.ExcluirProdutoBD(produto);
            }
            catch (Exception ex)
            {
                throw new Exception("Produto não excluido, tente novamente mais tarde.");
            }
           
            return RedirectToAction("Produtos", "Produtos");
        }
        public IActionResult SalvarProduto(ProdutoDomain.Entidades.Produtos produto)
        {
            try
            {
                this.SalvarProdutoController(produto);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Produtos", "Produtos");
        }

        public IActionResult Usuarios() {
            var usuarios = this.ListarUsuarios();
            return View(usuarios);
		}
        public IActionResult CriarEditarUsuario(int? id = null)
        {
           Usuario usuario  = new Usuario();
			if (id != null) usuario = this.ObtemUsuario(id.Value);
            return View(usuario);
        }
        public IActionResult DetalhesUsuario(int id)
        {
            Usuario usuario = this.ObtemUsuario(id);
            return View(usuario);
        }
        public IActionResult ExcluirUsuario(int id)
        {
            this.RemoverUsuario(id);
            return RedirectToAction("Usuarios", "Produtos");
        }
        public IActionResult SalvarUsuario(Usuario usuario,string senha2, bool? login = false) {
            try
            {
                if (usuario.senha != senha2) throw new Exception("As senhas não se coincidem!");
                this.SalvarUsuarioBD(usuario);
            }
            catch (Exception ex)
            {

                throw;
            }

            if (login != true) { 
                
                return RedirectToAction("Usuarios", "Produtos");
            }
            else
            {
				return RedirectToAction("Login", "Produtos");
			}
        }

        #endregion



		#region controllers
		public ProdutoDomain.Entidades.Produtos ObtemProduto(int id)
        {
            return _context.Produtos.SingleOrDefault(t => t.Id == id) ?? throw new Exception("Produto não encontrado ou não existe!");
        }
        public void ExcluirProdutoBD(ProdutoDomain.Entidades.Produtos produto) {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
        public void SalvarProdutoController(ProdutoDomain.Entidades.Produtos produto) {

            if (produto.Id == 0)
            {
                // Crio um novo produto
                _context.Produtos.Add(produto);
            }
            else
            {
                ProdutoDomain.Entidades.Produtos produtoBD = ObtemProduto(produto.Id);
                if (produto != null)
                {
                    produtoBD.Nome = produto.Nome;
                    produtoBD.Preco = produto.Preco;
                }
                else
                {
                    throw new Exception("Produto não encontrado.");
                }
            }
            _context.SaveChanges();
        }
        public Usuario ObtemUsuario(int id)
        {
            Usuario usuario = _context.Usuario.SingleOrDefault(u => u.Id == id) ?? throw new Exception("Usuário não encontrado ou não existe!");
            return usuario;
        }
        public void RemoverUsuario(int id)
        {
            Usuario usuario = this.ObtemUsuario(id);
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }
        public List<Usuario> ListarUsuarios()
        {
            var usuarios = _context.Usuario.ToList();
            return usuarios;
        }
        public void SalvarUsuarioBD(Usuario usuario) {
            if(usuario.Id == 0)
            {
				usuario.senha = _criptografiaService.HashSenha(usuario.senha);
				_context.Usuario.Add(usuario);
            }
            else
            {
               Usuario up = this.ObtemUsuario(usuario.Id);
                up.nome = usuario.nome;
                up.email = usuario.email;
                up.senha = _criptografiaService.HashSenha(usuario.senha);
                up.cep = usuario.cep;
                up.cpf = usuario.cpf;
                up.logradouro = usuario.logradouro;
                up.cidade = usuario.cidade;
                up.bairro = usuario.bairro;
                up.estado = usuario.estado;
            }
            _context.SaveChanges();
        }
        #endregion
    }
}
