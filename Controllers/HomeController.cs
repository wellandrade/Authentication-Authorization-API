using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("v1")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("popular-banco")]
        public async Task<ActionResult<dynamic>> Get([FromServices] DataContext contexto)
        {
            var funcionario = new Usuario() { Id = 1, Nome = "robin", Perfil = "funcionario" };
            var gerente = new Usuario() { Id = 2, Nome = "batman", Perfil = "gerente" };
            var categoria = new Categoria() { Id = 1, Titulo = "Informatica" };
            var produto = new Produto() { Id = 1, Categoria = categoria, Titulo = "Mouse", Preco = 299, Descricao = "Mouse" };

            contexto.Usuarios.Add(funcionario);
            contexto.Usuarios.Add(gerente);
            contexto.Categorias.Add(categoria);
            contexto.Produtos.Add(produto);
            await contexto.SaveChangesAsync();

            return Ok(new
            {
                message = "Dados configurados"
            });
        }
    }
}
