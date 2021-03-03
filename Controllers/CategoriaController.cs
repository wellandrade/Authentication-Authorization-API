using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    [Route("v1/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext contexto)
        {
            // AsNoTracking - desliga toda a parte de rastreamente e faz a consulta mais rapido
            var categorias = await contexto.Categorias.AsNoTracking().ToListAsync();

            return Ok(categorias);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Categoria>> GetById(int id, 
            [FromServices] DataContext contexto)
        {
            var categoria = await contexto.Categorias.FirstOrDefaultAsync(x => x.Id == id);
            
            return Ok(categoria);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<Categoria>> Post(
            [FromBody] Categoria model,
            [FromServices] DataContext contexto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                contexto.Categorias.Add(model);
                await contexto.SaveChangesAsync();

                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Nao foi possível criar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<Categoria>> Put(
            int id,
            [FromBody] Categoria categoria,
            [FromServices] DataContext contexto)
        {
            if (id != categoria.Id)
                return NotFound(new { message = "Categoria nao encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                contexto.Entry<Categoria>(categoria).State = EntityState.Modified;
                await contexto.SaveChangesAsync();

                return Ok(categoria);
            }
            catch
            {
                return BadRequest(new { message = "Nao foi possivel atualizar a categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "funcionario")]
        public async Task<ActionResult<List<Categoria>>> Delete(
            int id,
            [FromServices] DataContext contexto)
        {

            var categoria = await contexto.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
                return NotFound("Nao foi possivel localizar a categoria");

            try
            {
                contexto.Categorias.Remove(categoria);
                await contexto.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso" });
            }
            catch 
            {
                return BadRequest(new { message = "Nao foi possivel excluir a categoria" });
            }


        }
    }
}
