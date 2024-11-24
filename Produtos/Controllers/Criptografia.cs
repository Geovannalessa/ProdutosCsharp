using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.IO;
using System;
using System.Text;

namespace Produtos.Controllers
{
	public class Criptografia : Controller
	{
		public class CriptografiaService
		{
			// Chave fixa de 32 bytes para AES-256
			private readonly byte[] chaveFixa = Convert.FromBase64String("ChD+jPHfBTK0bMDjSPQZaAhstWuDzpiH357WRTTs3Hg=");

			// Criptografa a senha
			public string CriptografarSenha(string senha)
			{
				return Criptografar(senha);
			}

			// Descriptografa a senha
			public string DescriptografarSenha(string senhaCriptografada)
			{
				return Descriptografar(senhaCriptografada);
			}

			// Método para criptografar
			private string Criptografar(string textoClaro)
			{
				using (var aes = Aes.Create())
				{
					aes.Key = chaveFixa;
					aes.GenerateIV(); // Gerar IV automaticamente
					byte[] iv = aes.IV;

					using (var encryptor = aes.CreateEncryptor())
					using (var ms = new MemoryStream())
					{
						ms.Write(iv, 0, iv.Length); // Armazenar IV no início

						using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
						using (var sw = new StreamWriter(cs))
						{
							sw.Write(textoClaro);
						}

						return Convert.ToBase64String(ms.ToArray());
					}
				}
			}

			// Método para descriptografar
			public string Descriptografar(string textoCifrado)
			{
				byte[] bytesCifrados = Convert.FromBase64String(textoCifrado);

				using (var aes = Aes.Create())
				{
					aes.Key = chaveFixa; // A chave deve ser válida (32 bytes)

					// Extrai o IV do início do texto cifrado
					byte[] iv = new byte[aes.BlockSize / 8];
					Array.Copy(bytesCifrados, 0, iv, 0, iv.Length);
					aes.IV = iv;

					// Resto dos dados cifrados
					byte[] textoCifradoSemIV = new byte[bytesCifrados.Length - iv.Length];
					Array.Copy(bytesCifrados, iv.Length, textoCifradoSemIV, 0, textoCifradoSemIV.Length);

					using (var ms = new MemoryStream(textoCifradoSemIV))
					using (var decryptor = aes.CreateDecryptor())
					using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
					using (var sr = new StreamReader(cs))
					{
						return sr.ReadToEnd();
					}
				}
			}

			public  string HashSenha(string senha)
			{
				using (SHA256 sha256 = SHA256.Create())
				{
					byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
					StringBuilder builder = new StringBuilder();
					foreach (byte b in bytes)
					{
						builder.Append(b.ToString("x2"));
					}
					return builder.ToString();
				}
			}
		}
	}
}
