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
    public class TiendasController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public TiendasController(disconCTX _ctx)
        {
            ctx = _ctx;
        }
        
        [HttpPost("Listar")]
        public async Task<IActionResult> Get(Tiendas ts)
        {
            if (ts.IdTienda == 0)
            {
                List<Tiendas> tiendasList = await ctx.Tiendas.ToListAsync();
                List<Tiendas> listTiendas = new List<Tiendas>();
                foreach (var tienda in tiendasList)
                {
                    Tiendas tiendasls = new Tiendas
                    {
                        IdTienda = tienda.IdTienda,
                        NombreTienda = tienda.NombreTienda,
                        EstadoTienda = tienda.EstadoTienda
                    };

                    listTiendas.Add(tiendasls);
                }

                reply.ok = true;
                reply.data = listTiendas;

                return Ok(reply);
            }
            else
            {
                var tiendas = await ctx.Tiendas.FirstOrDefaultAsync(e => e.IdTienda == ts.IdTienda);

                if (tiendas == null)
                {
                    reply.ok = false;
                    reply.data = "No encontrado";

                    return Ok(reply);
                }
                else
                {

                    Tiendas lsTiend = new Tiendas();
                    lsTiend.IdTienda = tiendas.IdTienda;
                    lsTiend.NombreTienda = tiendas.NombreTienda;
                    lsTiend.EstadoTienda = tiendas.EstadoTienda;


                    reply.ok = true;
                    reply.data = tiendas;

                    return Ok(reply);
                }
            }
    }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Tiendas t)
        {
            try
            {
                var u = await ctx.Tiendas.FirstOrDefaultAsync(e => e.NombreTienda == t.NombreTienda);
                //Insertar
                if (t.IdTienda == 0 && u != null)//nombre existe
                {

                    reply.ok = false;
                    reply.data = "Nombre de tienda en uso";

                }

                else if (t.IdTienda == 0 && u == null) //nombre NO existe
                {
                    ctx.Tiendas.Add(t);

                    reply.ok = true;
                    reply.data = t;
                }
                //Actualizar
                else if (t.IdTienda != 0 && u == null) //nombre NO existe
                {
                    var userName = await ctx.Tiendas.FirstOrDefaultAsync(e => e.IdTienda == t.IdTienda);

                    userName.IdTienda = t.IdTienda;
                    userName.NombreTienda = t.NombreTienda;
                    userName.EstadoTienda = t.EstadoTienda;


                    ctx.Entry(userName).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = t;
                }
                else if (t.IdTienda != 0 && u != null && t.IdTienda != u.IdTienda) //nombre SI existe
                {
                    reply.ok = false;
                    reply.data = "Nombre de tienda en uso";

                }
                else if (t.IdTienda != 0 && u != null && t.IdTienda == u.IdTienda) //nombre es el mismo que ya tenía
                {

                    var userName = await ctx.Tiendas.FirstAsync(e => e.IdTienda == u.IdTienda);

                    userName.IdTienda = t.IdTienda;
                    userName.NombreTienda = t.NombreTienda;
                    userName.EstadoTienda = t.EstadoTienda;

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
