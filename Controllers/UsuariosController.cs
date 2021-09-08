using api_DISCON.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public UsuariosController(disconCTX _ctx) => ctx = _ctx;


        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Usuarios u)
        {
            if (u.IdUsuario == 0)
            {
                List<Usuarios> listaUser = await ctx.Usuarios.ToListAsync();
                List<Usuarios> UsuarioList = new List<Usuarios>();

                foreach (var usuario in listaUser)
                {

                    Usuarios userLis = new Usuarios
                    {
                        IdUsuario = usuario.IdUsuario,
                        IdCreden = usuario.IdCreden,
                        NombreUsuario = usuario.NombreUsuario,
                        EmailUsuario = usuario.EmailUsuario,
                        EstadoUsuario = usuario.EstadoUsuario,
                    };

                    UsuarioList.Add(userLis);
                }

                reply.ok = true;
                reply.data = UsuarioList;

                return Ok(reply);
            }
            else
            {
                var usuario = await ctx.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == u.IdUsuario);

                if (usuario == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {
                    Usuarios lsUsuario = new Usuarios();

                    lsUsuario.IdUsuario = usuario.IdUsuario;
                    lsUsuario.IdCreden = usuario.IdCreden;
                    lsUsuario.NombreUsuario = usuario.NombreUsuario;
                    lsUsuario.EmailUsuario = usuario.EmailUsuario;
                    lsUsuario.EstadoUsuario = usuario.EstadoUsuario;

                    reply.ok = true;
                    reply.data = usuario;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Usuarios us)
        {
            try
            {

                //Validacion ID Creden

                if (us.IdUsuario == 0) // insert 
                {
                    var creden = await ctx.Credenciales.FirstOrDefaultAsync(e => e.IdCreden == us.IdCreden);
                    var user = await ctx.Usuarios.FirstOrDefaultAsync(e => e.IdCreden == us.IdCreden);

                    if (user == null && creden != null)
                    {
                        ctx.Usuarios.Add(us);

                        reply.ok = true;
                        reply.data = us;
                    }
                    else
                    {
                        reply.ok = false;
                        reply.data = user != null
                            ? "Ya existe un usuario con esta id de credencial"
                            : "No existe una credencial con el id seleccionado";
                    }
                }
                else if (us.IdUsuario != 0) // update 
                {
                    var userName = await ctx.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == us.IdUsuario);

                    userName.IdUsuario = us.IdUsuario;
                    userName.IdCreden = us.IdCreden;
                    userName.NombreUsuario = us.NombreUsuario;
                    userName.EmailUsuario = us.EmailUsuario;
                    userName.EstadoUsuario = us.EstadoUsuario;


                    ctx.Entry(userName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = us;
                }
                //nombre SI existe


                await ctx.SaveChangesAsync();
                return Ok(reply);


            }
            catch
            {
                reply.ok = false;
                reply.data = "Ocurrio un error.";
                return BadRequest(reply);
            }


        }
    }
}
