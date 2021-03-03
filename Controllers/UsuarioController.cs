using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Services;
using System.Collections.Generic;

namespace Shop.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> Post(
        [FromBody] Usuario model,
        [FromServices] DataContext contexto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model.Perfil = "funcionario";

                contexto.Usuarios.Add(model);
                await contexto.SaveChangesAsync();

                model.Senha = "";

                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Nao foi possivel criar o usuario" });
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromBody] Usuario model,
            [FromServices] DataContext contexto)
        {
            var usuario = await contexto
                .Usuarios
                .AsNoTracking()
                .Where(x => x.Nome == model.Nome && x.Senha == model.Senha)
                .FirstOrDefaultAsync();

            if (usuario == null)
                return NotFound(new { message = "Usuario ou senhas invalidos" });

            var token = TokenService.GerarToken(usuario);

            return new
            {
                usuario = usuario,
                token = token
            };
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "gerente")]
        public async Task<ActionResult<Usuario>> Get([FromServices] DataContext contexto)
        {
            var usuarios = await contexto
                .Usuarios
                .AsNoTracking()
                .ToListAsync();

            if (usuarios == null)
                return NotFound(usuarios);

            return Ok(usuarios);
        }
    }
}
