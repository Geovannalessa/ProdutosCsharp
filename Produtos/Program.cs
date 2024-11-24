using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using ProdutoContext;
using static Produtos.Controllers.Criptografia;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>();
builder.Services.AddSingleton<CriptografiaService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Produtos}/{action=Login}/{id?}");
app.Run();
