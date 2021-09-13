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
    public class ModuloClasemodController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public ModuloClasemodController(disconCTX _ctx) => ctx = _ctx;

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(ModuloClasemod ts)
        {
            if (ts.IdModulo == 0 && ts.IdClasmod == 0)
            {
                List<ModuloClasemod> tiendasList = await ctx.ModuloClasemod.ToListAsync();
                List<ModuloClasemod> listTiendas = new List<ModuloClasemod>();
                foreach (var tienda in tiendasList)
                {
                    ModuloClasemod tiendasls = new ModuloClasemod
                    {
                        IdModclas = tienda.IdModclas,
                        IdClasmod = tienda.IdClasmod,
                        IdModulo = tienda.IdModulo
                    };

                    listTiendas.Add(tiendasls);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (ts.IdModulo == 0 && ts.IdClasmod != 0)
            {
                var listTiendas = await ctx.ModuloClasemod.Where(e => e.IdClasmod == ts.IdClasmod).ToListAsync();
                List<ModuloClasemod> tiendaList = new List<ModuloClasemod>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa clase registrada";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        ModuloClasemod tiendasls = new ModuloClasemod
                        {
                            IdModclas = tienda.IdModclas,
                            IdClasmod = tienda.IdClasmod,
                            IdModulo = tienda.IdModulo
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            else if (ts.IdModulo != 0 && ts.IdClasmod != 0)
            {
                var tienda = await ctx.ModuloClasemod.FirstOrDefaultAsync(e => e.IdModulo == ts.IdModulo && e.IdClasmod == ts.IdClasmod);

                if (tienda == null)
                {
                    reply.ok = false;
                    reply.data = "Modulo no encontrado";

                }
                else
                {
                    ModuloClasemod tiendals = new ModuloClasemod
                    {
                        IdModclas = tienda.IdModclas,
                        IdClasmod = tienda.IdClasmod,
                        IdModulo = tienda.IdModulo
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (ts.IdModulo != 0 && ts.IdClasmod == 0)
            {
                var listTiendas = await ctx.ModuloClasemod.Where(e => e.IdModulo == ts.IdModulo).ToListAsync();
                List<ModuloClasemod> tiendaList = new List<ModuloClasemod>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese modulo registrado";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        ModuloClasemod tiendasls = new ModuloClasemod
                        {
                            IdModclas = tienda.IdModclas,
                            IdClasmod = tienda.IdClasmod,
                            IdModulo = tienda.IdModulo
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            return Ok(reply);
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post(ModuloClasemod t)
        {
            if (t.IdModclas == 0)
            {
                    ctx.ModuloClasemod.Add(t);
                    reply.ok = true;
                    reply.data = t;
            }
            else
            {
                var finName = await ctx.ModuloClasemod.FirstOrDefaultAsync(e => e.IdModclas == t.IdModclas);


                finName.IdModclas = t.IdModclas;
                finName.IdModulo = t.IdModulo;
                finName.IdClasmod = t.IdClasmod;



                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = t;
            }
            await ctx.SaveChangesAsync();
            return Created("Respuesta", reply);
        }

    }
}
