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
    public class SeccionesArticulosController : ControllerBase
    {
        private readonly disconCTX ctx;
        Respuesta reply = new Respuesta();

        public SeccionesArticulosController(disconCTX _ctx)
        {
            ctx = _ctx;
        }

        [HttpPost("Listar")]
        public async Task<IActionResult> Get(SeccionesArticulos sa)
        {
            if (sa.IdSeccion == 0 && sa.IdArticulo == 0)
            {
                List<SeccionesArticulos> seccionesList = await ctx.SeccionesArticulos.ToListAsync();
                List<SeccionesArticulos> listSecciones = new List<SeccionesArticulos>();
                foreach (var seccion in seccionesList)
                {
                    SeccionesArticulos seccionls = new SeccionesArticulos
                    {
                        IdSeccart = seccion.IdSeccart,
                        IdSeccion = seccion.IdSeccion,
                        IdArticulo = seccion.IdArticulo,
                    };

                    listSecciones.Add(seccionls);
                }

                reply.ok = true;
                reply.data = listSecciones;
            }
            else if (sa.IdSeccion == 0 && sa.IdArticulo != 0)
            {
                var listSeccion = await ctx.SeccionesArticulos.Where(e => e.IdArticulo == sa.IdArticulo).ToListAsync();
                List<SeccionesArticulos> seccionList = new List<SeccionesArticulos>();

                if (listSeccion.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese producto registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var seccion in listSeccion)
                    {
                        SeccionesArticulos seccionls = new SeccionesArticulos
                        {
                            IdSeccart = seccion.IdSeccart,
                            IdSeccion = seccion.IdSeccion,
                            IdArticulo = seccion.IdArticulo,
                        };

                        seccionList.Add(seccionls);
                    }

                    reply.ok = true;
                    reply.data = seccionList;
                    return Ok(reply);
                }
            }
            else if (sa.IdSeccion != 0 && sa.IdArticulo != 0)
            {
                var secciones = await ctx.SeccionesArticulos.FirstOrDefaultAsync(e => e.IdSeccion == sa.IdSeccion && e.IdArticulo == sa.IdArticulo);

                if (secciones == null)
                {
                    reply.ok = false;
                    reply.data = "Seccion no encontrada";

                }
                else
                {
                    SeccionesArticulos tiendals = new SeccionesArticulos
                    {
                        IdSeccart = secciones.IdSeccart,
                        IdSeccion = secciones.IdSeccion,
                        IdArticulo = secciones.IdArticulo,
                    };

                    reply.ok = true;
                    reply.data = tiendals;
                }
            }
            else if (sa.IdSeccion != 0 && sa.IdArticulo == 0)
            {
                var listSecciones = await ctx.SeccionesArticulos.Where(e => e.IdSeccion == sa.IdSeccion).ToListAsync();
                List<SeccionesArticulos> seccList = new List<SeccionesArticulos>();

                if (listSecciones.Count() == 0)
                {
                    reply.ok = false;
                    reply.data = "No existe ese articulo registrados";

                    return Ok(reply);
                }
                else
                {
                    foreach (var secciones in listSecciones)
                    {
                        SeccionesArticulos seccionls = new SeccionesArticulos
                        {
                            IdSeccart = secciones.IdSeccart,
                            IdSeccion = secciones.IdSeccion,
                            IdArticulo = secciones.IdArticulo,
                        };

                        seccList.Add(seccionls);
                    }

                    reply.ok = true;
                    reply.data = seccList;
                    return Ok(reply);
                }
            }
            return Ok(reply);
        }

        [HttpPost("InsertarActualizar")]
        public async Task<IActionResult> Post([FromBody] SeccionesArticulos sar)
        {
            var seccion = await ctx.SeccionesArticulos.FirstOrDefaultAsync(e => e.IdSeccion == sar.IdSeccion);
            var articulo = await ctx.SeccionesArticulos.FirstOrDefaultAsync(e => e.IdArticulo == sar.IdArticulo);
            if (sar.IdSeccart == 0 && sar.IdSeccion != 0 && sar.IdArticulo != 0)
            {
                if ( seccion == null )
                {
                    reply.ok = false;
                    reply.data = "No existe esa seccion";

                    return Ok(reply);

                }else if ( articulo == null )
                {
                    reply.ok = false;
                    reply.data = "No existe ese articulo";

                    return Ok(reply);
                }
                else
                {
                    ctx.SeccionesArticulos.Add(sar);
                    reply.ok = true;
                    reply.data = sar;
                }
                await ctx.SaveChangesAsync();
            }
            else if (sar.IdSeccart != 0 && sar.IdSeccion != 0 && sar.IdArticulo != 0)
            {
                var seccart = await ctx.SeccionesArticulos.FirstOrDefaultAsync(e => e.IdSeccart == sar.IdSeccart);
                if (seccart == null)
                {
                    reply.ok = false;
                    reply.data = "No existe ese elemento";

                    return Ok(reply);
                }
                else
                {
                    seccart.IdSeccart = sar.IdSeccart;
                    seccart.IdSeccion = sar.IdSeccion;
                    seccart.IdArticulo = sar.IdArticulo;


                    ctx.Entry(seccart).State = EntityState.Modified;
                    reply.ok = true;
                    reply.data = sar;
                }
            }
            await ctx.SaveChangesAsync();
            return Ok(reply);
        }
    }
}
