using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext contexto)
        {
            var produtos = await contexto
                .Produtos
                .Include(x => x.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Categoria>> GetById(
            int id,
            [FromServices] DataContext contexto)
        {
            var produto = await contexto
                .Produtos
                .Include(x => x.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(produto);
        }

        [HttpGet]
        [Route("categorias/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> GetByIdCategoria(
            int id,
            [FromServices] DataContext contexto)
        {
            var produto = await contexto
                .Produtos
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.CategoriaId == id)
                .ToListAsync();

            return Ok(produto);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Funcionario")]
        public async Task<ActionResult<Produto>> Post(
        [FromBody] Produto model,
        [FromServices] DataContext contexto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                contexto.Produtos.Add(model);
                await contexto.SaveChangesAsync();

                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Nao foi possível criar o produto" });
            }
        }

    }
}
