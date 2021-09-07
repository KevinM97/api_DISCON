using api_DISCON.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredencialesController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public CredencialesController(disconCTX _ctx) => ctx = _ctx;

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Credenciales c)
        {
            if (c.IdCreden == 0)
            {
                List<Credenciales> listaCreden = await ctx.Credenciales.ToListAsync();
                List<Credenciales> credenList = new List<Credenciales>();

                foreach (var credencial in listaCreden)
                {

                    Credenciales credenLis = new Credenciales
                    {
                        IdCreden = credencial.IdCreden,
                        UsernameCreden = credencial.UsernameCreden,
                        PasswordCreden = credencial.PasswordCreden
                    };

                    credenList.Add(credenLis);
                }

                reply.ok = true;
                reply.data = credenList;

                return Ok(reply);
            }
            else
            {
                var credencial = await ctx.Credenciales.FirstOrDefaultAsync(e => e.IdCreden == c.IdCreden);

                if (credencial == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Credenciales lsCreden = new Credenciales();
                    lsCreden.IdCreden = credencial.IdCreden;
                    lsCreden.UsernameCreden = credencial.UsernameCreden;
                    lsCreden.PasswordCreden = credencial.PasswordCreden;

                    reply.ok = true;
                    reply.data = credencial;

                    return Ok(reply);
                }
            }
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Credenciales cr)
        {
            try
            {
                var u = await ctx.Credenciales.FirstOrDefaultAsync(e => e.UsernameCreden == cr.UsernameCreden);
                //Insertar
                if (cr.IdCreden == 0 && u != null)//nombre existe
                {

                    reply.ok = false;
                    reply.data = "Nombre de usuario en uso";

                }

                else if (cr.IdCreden == 0 && u == null) //nombre NO existe
                {
                    ctx.Credenciales.Add(cr);

                    reply.ok = true;
                    reply.data = cr;
                }
                //Actualizar
                else if (cr.IdCreden != 0 && u == null) //nombre NO existe
                {
                    var userName = await ctx.Credenciales.FirstOrDefaultAsync(e => e.IdCreden == cr.IdCreden);

                    userName.IdCreden = cr.IdCreden;
                    userName.UsernameCreden = cr.UsernameCreden;
                    userName.PasswordCreden = cr.PasswordCreden;


                    ctx.Entry(userName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = cr;
                }
                else if (cr.IdCreden != 0 && u != null && cr.IdCreden != u.IdCreden) //nombre SI existe
                {
                    reply.ok = false;
                    reply.data = "Nombre de usuario en uso";

                }
                else if (cr.IdCreden != 0 && u != null && cr.IdCreden == u.IdCreden) //nombre es el mismo que ya tenía
                {

                    var userName = await ctx.Credenciales.FirstAsync(e => e.IdCreden == u.IdCreden);

                    userName.IdCreden = cr.IdCreden;
                    userName.UsernameCreden = cr.UsernameCreden;
                    userName.PasswordCreden = cr.PasswordCreden;

                    ctx.Entry(userName).CurrentValues.SetValues(userName);

                    reply.ok = true;
                    reply.data = userName;

                }
                await ctx.SaveChangesAsync();
                return Ok(reply);
            }
            catch
            {
                reply.ok = false;
                reply.data = "Error";
                return BadRequest(reply);
            }
        }
    }
}
