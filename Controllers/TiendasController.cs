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
        public async Task<IActionResult> Get(Tiendas t)
        {
            if (t.IdTienda == 0 && t.IdProducto == 0)
            {
                List<Tiendas> tiendasList = await ctx.Tiendas.ToListAsync();
                List<Tiendas> listTiendas = new List<Tiendas>();
                foreach (var tienda in tiendasList)
                {
                    Tiendas tiendasls = new Tiendas
                    {
                        IdTienda = tienda.IdTienda,
                        IdProducto = tienda.IdProducto,
                        NombreTienda = tienda.NombreTienda
                    };

                    listTiendas.Add(tiendasls);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (t.IdTienda == 0 && t.IdProducto!= 0)
            {
                var listTiendas = await ctx.Tiendas.Where(e => e.IdProducto == t.IdProducto).ToListAsync();
                List<Tiendas> tiendaList = new List<Tiendas>();

                if(listTiendas.Count()==0)
                {
                    reply.ok = false;
                    reply.data = "No hay productos registradas";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        Tiendas tiendasls = new Tiendas
                        {
                            IdTienda = tienda.IdTienda,
                            IdProducto = tienda.IdProducto,
                            NombreTienda = tienda.NombreTienda
                        };

                        listTiendas.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                return Ok(reply);
                }
            }
        else if (t.IdTienda != 0 && t.IdProducto != 0)
            {
                var tiendas = await ctx.Tiendas.FirstOrDefaultAsync(e => e.IdTienda == t.IdTienda && e.IdProducto == t.IdProducto);

                if(tiendas == null)
                {
                    reply.ok = false;
                    reply.data = "Tienda no encontrada";

                }
                else
                {
                    Tiendas tiendals = new Tiendas
                    {
                        IdTienda = tiendas.IdTienda,
                        IdProducto = tiendas.IdProducto,
                        NombreTienda = tiendas.NombreTienda
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            return Ok(reply);
    }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] Tiendas t)
        {

            if (t.IdTienda == 0)
            {
                ctx.Tiendas.Add(t);

                reply.ok = true;
                reply.data = t;
            }
            else if (t.IdTienda != 0)
            {
                var finName = await ctx.Tiendas.FirstOrDefaultAsync(e => e.IdTienda == t.IdTienda);


                finName.IdTienda = t.IdTienda;
                finName.IdProducto = t.IdProducto;
                finName.NombreTienda = t.NombreTienda;


                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = t;
            }
            await ctx.SaveChangesAsync();
            return Created("Venta", reply);



        }
    }
}
