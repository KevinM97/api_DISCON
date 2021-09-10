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
    public class RespuestaPreguntaController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public RespuestaPreguntaController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(RespuestaPregunta ts)
        {
            if (ts.IdPregunta == 0 && ts.IdRespuesta == 0)
            {
                List<RespuestaPregunta> tiendasList = await ctx.RespuestaPregunta.ToListAsync();
                List<RespuestaPregunta> listTiendas = new List<RespuestaPregunta>();
                foreach (var tienda in tiendasList)
                {
                    RespuestaPregunta tiendasls = new RespuestaPregunta
                    {
                        IdPregresp = tienda.IdPregresp,
                        IdPregunta = tienda.IdPregunta,
                        IdRespuesta = tienda.IdRespuesta
                    };

                    listTiendas.Add(tiendasls);
                }

                reply.ok = true;
                reply.data = listTiendas;
            }
            else if (ts.IdPregunta == 0 && ts.IdRespuesta != 0)
            {
                var listTiendas = await ctx.RespuestaPregunta.Where(e => e.IdRespuesta == ts.IdRespuesta).ToListAsync();
                List<RespuestaPregunta> tiendaList = new List<RespuestaPregunta>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa respuesta registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        RespuestaPregunta tiendasls = new RespuestaPregunta
                        {
                            IdPregresp = tienda.IdPregresp,
                            IdPregunta = tienda.IdPregunta,
                            IdRespuesta = tienda.IdRespuesta
                        };

                        tiendaList.Add(tiendasls);
                    }

                    reply.ok = true;
                    reply.data = tiendaList;
                    return Ok(reply);
                }
            }
            else if (ts.IdPregunta != 0 && ts.IdRespuesta != 0)
            {
                var tiendas = await ctx.RespuestaPregunta.FirstOrDefaultAsync(e => e.IdPregunta == ts.IdPregunta && e.IdRespuesta == ts.IdRespuesta);

                if (tiendas == null)
                {
                    reply.ok = false;
                    reply.data = "Pregunta no encontrada";

                }
                else
                {
                    RespuestaPregunta tiendals = new RespuestaPregunta
                    {
                        IdPregresp = tiendas.IdPregresp,
                        IdPregunta = tiendas.IdPregunta,
                        IdRespuesta = tiendas.IdRespuesta
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (ts.IdPregunta != 0 && ts.IdRespuesta == 0)
            {
                var listTiendas = await ctx.RespuestaPregunta.Where(e => e.IdPregunta == ts.IdPregunta).ToListAsync();
                List<RespuestaPregunta> tiendaList = new List<RespuestaPregunta>();

                if (listTiendas.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe esa pregunta registrada";

                    return Ok(reply);
                }
                else
                {
                    foreach (var tienda in listTiendas)
                    {
                        RespuestaPregunta tiendasls = new RespuestaPregunta
                        {
                            IdPregresp = tienda.IdPregresp,
                            IdPregunta = tienda.IdPregunta,
                            IdRespuesta = tienda.IdRespuesta
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
        public async Task<IActionResult> Post([FromBody] RespuestaPregunta t)
        {
            if (t.IdPregresp == 0)
            {
                var pregunta = await ctx.RespuestaPregunta.FirstOrDefaultAsync(e => e.IdPregunta == t.IdPregunta);
                var respuesta = await ctx.RespuestaPregunta.FirstOrDefaultAsync(e => e.IdRespuesta == t.IdRespuesta);

                if (pregunta == null)
                {
                    reply.ok = false;
                    reply.data = "No existe esa pregunta";

                    return Ok(reply);
                } 
                else if(respuesta == null)
                {
                    reply.ok = false;
                    reply.data = "No existe esa respuesta";

                    return Ok(reply);
                }
                else
                {
                ctx.RespuestaPregunta.Add(t);

                reply.ok = true;
                reply.data = t;
                }
            }
            else if (t.IdPregresp != 0)
            {
                var finName = await ctx.RespuestaPregunta.FirstOrDefaultAsync(e => e.IdPregresp == t.IdPregresp);


                finName.IdPregresp = t.IdPregresp;
                finName.IdPregunta = t.IdPregunta;
                finName.IdRespuesta = t.IdRespuesta;


                ctx.Entry(finName).State = EntityState.Modified;
                reply.ok = true;
                reply.data = t;
            }
            await ctx.SaveChangesAsync();
            return Created("Respuesta", reply);
        }
    }
}
